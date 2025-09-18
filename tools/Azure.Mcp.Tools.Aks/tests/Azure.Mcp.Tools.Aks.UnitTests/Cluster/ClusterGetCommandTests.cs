// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using Azure.Mcp.Core.Models.Command;
using Azure.Mcp.Core.Options;
using Azure.Mcp.Tools.Aks.Commands.Cluster;
using Azure.Mcp.Tools.Aks.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Azure.Mcp.Tools.Aks.UnitTests.Cluster;

public class ClusterGetCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAksService _aksService;
    private readonly ILogger<ClusterGetCommand> _logger;
    private readonly ClusterGetCommand _command;
    private readonly CommandContext _context;
    private readonly Command _commandDefinition;

    public ClusterGetCommandTests()
    {
        _aksService = Substitute.For<IAksService>();
        _logger = Substitute.For<ILogger<ClusterGetCommand>>();

        var collection = new ServiceCollection().AddSingleton(_aksService);
        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _commandDefinition = _command.GetCommand();
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("get", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
    }

    [Theory]
    [InlineData("--subscription sub1 --resource-group rg1 --cluster cluster1", true)]
    [InlineData("--subscription sub1 --cluster cluster1", false)]  // Missing resource-group
    [InlineData("--subscription sub1 --resource-group rg1", false)]     // Missing cluster
    [InlineData("--resource-group rg1 --cluster cluster1", false)] // Missing subscription
    [InlineData("", false)]                                              // Missing all required options
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            var testCluster = new Models.Cluster
            {
                Name = "test-cluster",
                SubscriptionId = "sub1",
                ResourceGroupName = "rg1",
                Location = "East US"
            };

            _aksService.GetCluster(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<RetryPolicyOptions>())
                .Returns(testCluster);
        }

        var parseResult = _commandDefinition.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(shouldSucceed ? 200 : 400, response.Status);
        if (shouldSucceed)
        {
            Assert.NotNull(response.Results);
            Assert.Equal("Success", response.Message);
        }
        else
        {
            Assert.Contains("required", response.Message.ToLower());
        }
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsClusterWhenFound()
    {
        // Arrange
        var expectedCluster = new Models.Cluster
        {
            Name = "test-cluster",
            SubscriptionId = "test-subscription",
            ResourceGroupName = "test-rg",
            Location = "East US",
            KubernetesVersion = "1.28.0",
            ProvisioningState = "Succeeded"
        };

        _aksService.GetCluster("test-subscription", "test-cluster", "test-rg", null, Arg.Any<RetryPolicyOptions>())
            .Returns(expectedCluster);

        var parseResult = _commandDefinition.Parse(["--subscription", "test-subscription", "--resource-group", "test-rg", "--cluster", "test-cluster"]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        Assert.Equal("Success", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsNullWhenClusterNotFound()
    {
        // Arrange
        _aksService.GetCluster("test-subscription", "nonexistent-cluster", "test-rg", null, Arg.Any<RetryPolicyOptions>())
            .Returns((Models.Cluster?)null);

        var parseResult = _commandDefinition.Parse(["--subscription", "test-subscription", "--resource-group", "test-rg", "--cluster", "nonexistent-cluster"]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Null(response.Results);
        Assert.Equal("Success", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceErrors()
    {
        // Arrange
        _aksService.GetCluster(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromException<Models.Cluster?>(new Exception("Test error")));

        var parseResult = _commandDefinition.Parse(["--subscription", "test-subscription", "--resource-group", "test-rg", "--cluster", "test-cluster"]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_Handles404NotFound()
    {
        // Arrange
        var notFoundException = new RequestFailedException(404, "Not Found");
        _aksService.GetCluster(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromException<Models.Cluster?>(notFoundException));

        var parseResult = _commandDefinition.Parse(["--subscription", "test-subscription", "--resource-group", "test-rg", "--cluster", "test-cluster"]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(404, response.Status);
        Assert.Contains("AKS cluster not found", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_Handles403Forbidden()
    {
        // Arrange
        var forbiddenException = new RequestFailedException(403, "Forbidden");
        _aksService.GetCluster(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromException<Models.Cluster?>(forbiddenException));

        var parseResult = _commandDefinition.Parse(["--subscription", "test-subscription", "--resource-group", "test-rg", "--cluster", "test-cluster"]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(403, response.Status);
        Assert.Contains("Authorization failed", response.Message);
    }
}
