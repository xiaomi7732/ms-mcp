// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.Mcp.Core.Models.Command;
using Azure.Mcp.TestUtilities;
using Azure.Mcp.Tools.Postgres.Commands;
using Azure.Mcp.Tools.Postgres.Commands.Table;
using Azure.Mcp.Tools.Postgres.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Azure.Mcp.Tools.Postgres.UnitTests.Table;

public class TableSchemaGetCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPostgresService _postgresService;
    private readonly ILogger<TableSchemaGetCommand> _logger;

    public TableSchemaGetCommandTests()
    {
        _postgresService = Substitute.For<IPostgresService>();
        _logger = Substitute.For<ILogger<TableSchemaGetCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_postgresService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsSchema_WhenSchemaExists()
    {
        var expectedSchema = new List<string>(["CREATE TABLE test (id INT);"]);
        _postgresService.GetTableSchemaAsync("sub123", "rg1", "user1", "server1", "db123", "table123").Returns(expectedSchema);

        var command = new TableSchemaGetCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "sub123", "--resource-group", "rg1", "--user", "user1", "--server", "server1", "--database", "db123", "--table", "table123"]);
        var context = new CommandContext(_serviceProvider);

        var response = await command.ExecuteAsync(context, args);
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize(json, PostgresJsonContext.Default.TableSchemaGetCommandResult);
        Assert.NotNull(result);
        Assert.Equal(expectedSchema, result.Schema);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmpty_WhenSchemaDoesNotExist()
    {
        _postgresService.GetTableSchemaAsync("sub123", "rg1", "user1", "server1", "db123", "table123").Returns([]);

        var command = new TableSchemaGetCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "sub123", "--resource-group", "rg1", "--user", "user1", "--server", "server1", "--database", "db123", "--table", "table123"]);
        var context = new CommandContext(_serviceProvider);

        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.NotNull(response.Results);
        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize(json, PostgresJsonContext.Default.TableSchemaGetCommandResult);
        Assert.NotNull(result);
        Assert.Empty(result.Schema);
    }

    [Theory]
    [InlineData("--subscription")]
    [InlineData("--resource-group")]
    [InlineData("--user")]
    [InlineData("--server")]
    [InlineData("--database")]
    [InlineData("--table")]
    public async Task ExecuteAsync_ReturnsError_WhenParameterIsMissing(string missingParameter)
    {
        var command = new TableSchemaGetCommand(_logger);
        var args = command.GetCommand().Parse(ArgBuilder.BuildArgs(missingParameter,
            ("--subscription", "sub123"),
            ("--resource-group", "rg1"),
            ("--user", "user1"),
            ("--server", "server123"),
            ("--database", "db123"),
            ("--table", "table123")
        ));

        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Equal($"Missing Required options: {missingParameter}", response.Message);
    }
}
