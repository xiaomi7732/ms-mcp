// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using Azure.Mcp.Core.Models.Command;
using Azure.Mcp.Core.Options;
using Azure.Mcp.Tools.KeyVault.Commands.Certificate;
using Azure.Mcp.Tools.KeyVault.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Azure.Mcp.Tools.KeyVault.UnitTests.Certificate;

public class CertificateGetCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IKeyVaultService _keyVaultService;
    private readonly ILogger<CertificateGetCommand> _logger;
    private readonly CertificateGetCommand _command;
    private readonly CommandContext _context;
    private readonly Command _commandDefinition;

    private const string _knownSubscriptionId = "knownSubscription";
    private const string _knownVaultName = "knownVaultName";
    private const string _knownCertificateName = "knownCertificateName";

    public CertificateGetCommandTests()
    {
        _keyVaultService = Substitute.For<IKeyVaultService>();
        _logger = Substitute.For<ILogger<CertificateGetCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_keyVaultService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _commandDefinition = _command.GetCommand();
    }

    [Fact]
    public async Task ExecuteAsync_CallsServiceCorrectly()
    {
        // Arrange
        var expectedError = "Expected test error";

        // TODO (vcolin7): Find a way to mock KeyVaultCertificateWithPolicy
        // We'll test that the service is called correctly, but let it fail since mocking the return is complex
        _keyVaultService.GetCertificate(
            Arg.Is(_knownVaultName),
            Arg.Is(_knownCertificateName),
            Arg.Is(_knownSubscriptionId),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new Exception(expectedError));

        var args = _commandDefinition.Parse([
            "--vault", _knownVaultName,
            "--certificate", _knownCertificateName,
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert - Verify the service was called with correct parameters
        await _keyVaultService.Received(1).GetCertificate(
            Arg.Is(_knownVaultName),
            Arg.Is(_knownCertificateName),
            Arg.Is(_knownSubscriptionId),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>());

        // Should handle the exception
        Assert.Equal(500, response.Status);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsInvalidObject_IfCertificateNameIsEmpty()
    {
        // Arrange - No need to mock service since validation should fail before service is called
        var args = _commandDefinition.Parse([
            "--vault", _knownVaultName,
            "--certificate", "",
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert - Should return validation error response
        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Contains("required", response.Message.ToLower());
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        // Arrange
        var expectedError = "Test error";

        _keyVaultService.GetCertificate(
            Arg.Is(_knownVaultName),
            Arg.Is(_knownCertificateName),
            Arg.Is(_knownSubscriptionId),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new Exception(expectedError));

        var args = _commandDefinition.Parse([
            "--vault", _knownVaultName,
            "--certificate", _knownCertificateName,
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.StartsWith(expectedError, response.Message);
    }
}
