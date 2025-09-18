// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.Mcp.Tests;
using Azure.Mcp.Tests.Client;
using Xunit;

namespace Azure.Mcp.Tools.Aks.LiveTests;

public sealed class NodepoolGetCommandTests(ITestOutputHelper output)
    : CommandTestsBase(output)
{
    [Fact]
    public async Task Should_get_nodepool_for_cluster()
    {
        // Get a real cluster to target
        var listResult = await CallToolAsync(
            "azmcp_aks_cluster_list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        var clusters = listResult.AssertProperty("clusters");
        Assert.True(clusters.GetArrayLength() > 0, "Expected at least one AKS cluster for testing nodepool get command");

        var firstCluster = clusters.EnumerateArray().First();
        var clusterName = firstCluster.GetProperty("name").GetString()!;
        var resourceGroupName = firstCluster.GetProperty("resourceGroupName").GetString()!;

        // Find a node pool to query
        var nodepoolList = await CallToolAsync(
            "azmcp_aks_nodepool_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", resourceGroupName },
                { "cluster", clusterName }
            });

        var nodePools = nodepoolList.AssertProperty("nodePools");
        Assert.True(nodePools.GetArrayLength() > 0, "Expected at least one node pool in the cluster");

        var firstPool = nodePools.EnumerateArray().First();
        var nodepoolName = firstPool.GetProperty("name").GetString()!;

        // Get details for that node pool
        var nodepoolGet = await CallToolAsync(
            "azmcp_aks_nodepool_get",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", resourceGroupName },
                { "cluster", clusterName },
                { "nodepool", nodepoolName }
            });

        var nodePool = nodepoolGet.AssertProperty("nodePool");
        Assert.Equal(JsonValueKind.Object, nodePool.ValueKind);
        Assert.Equal(nodepoolName, nodePool.GetProperty("name").GetString());

        if (nodePool.TryGetProperty("mode", out var modeProperty))
        {
            Assert.False(string.IsNullOrEmpty(modeProperty.GetString()));
        }

        if (nodePool.TryGetProperty("provisioningState", out var stateProperty))
        {
            Assert.False(string.IsNullOrEmpty(stateProperty.GetString()));
        }

        nodePool.AssertProperty("orchestratorVersion");
        nodePool.AssertProperty("currentOrchestratorVersion");
        nodePool.AssertProperty("enableAutoScaling");
        nodePool.AssertProperty("maxPods");
        nodePool.AssertProperty("osSKU");
        nodePool.AssertProperty("nodeImageVersion");
    }

    [Fact]
    public async Task Should_handle_nonexistent_nodepool_gracefully()
    {
        var result = await CallToolAsync(
            "azmcp_aks_nodepool_get",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", "nonexistent-rg" },
                { "cluster", "nonexistent-cluster" },
                { "nodepool", "nonexistent-nodepool" }
            });

        // Should return runtime error details in results
        Assert.True(result.HasValue);
        var errorDetails = result.Value;
        errorDetails.AssertProperty("message");
        var typeProperty = errorDetails.AssertProperty("type");
        Assert.Equal("Exception", typeProperty.GetString());
    }

    [Fact]
    public async Task Should_validate_required_parameters()
    {
        // Missing nodepool
        var r1 = await CallToolAsync(
            "azmcp_aks_nodepool_get",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", "rg" },
                { "cluster", "cluster" }
            });
        Assert.False(r1.HasValue);

        // Missing cluster
        var r2 = await CallToolAsync(
            "azmcp_aks_nodepool_get",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", "rg" },
                { "nodepool", "np1" }
            });
        Assert.False(r2.HasValue);

        // Missing resource-group
        var r3 = await CallToolAsync(
            "azmcp_aks_nodepool_get",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "cluster", "cluster" },
                { "nodepool", "np1" }
            });
        Assert.False(r3.HasValue);

        // Missing subscription
        var r4 = await CallToolAsync(
            "azmcp_aks_nodepool_get",
            new()
            {
                { "resource-group", "rg" },
                { "cluster", "cluster" },
                { "nodepool", "np1" }
            });
        Assert.False(r4.HasValue);
    }

    [Fact]
    public async Task Should_handle_invalid_subscription_gracefully()
    {
        var result = await CallToolAsync(
            "azmcp_aks_nodepool_get",
            new()
            {
                { "subscription", "invalid-subscription" },
                { "resource-group", "rg" },
                { "cluster", "cluster" },
                { "nodepool", "np1" }
            });

        Assert.True(result.HasValue);
        var errorDetails = result.Value;
        errorDetails.AssertProperty("message");
        var typeProperty = errorDetails.AssertProperty("type");
        Assert.Equal("Exception", typeProperty.GetString());
    }

    [Fact]
    public async Task Should_handle_empty_subscription_gracefully()
    {
        var result = await CallToolAsync(
            "azmcp_aks_nodepool_get",
            new()
            {
                { "subscription", "" },
                { "resource-group", "rg" },
                { "cluster", "cluster" },
                { "nodepool", "np1" }
            });

        // Should return validation error response with no results
        Assert.False(result.HasValue);
    }
}
