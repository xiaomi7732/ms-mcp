// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Mcp.Core.Commands;
using Azure.Mcp.Core.Commands.Subscription;
using Azure.Mcp.Core.Extensions;
using Azure.Mcp.Tools.ServiceBus.Models;
using Azure.Mcp.Tools.ServiceBus.Options;
using Azure.Mcp.Tools.ServiceBus.Options.Topic;
using Azure.Mcp.Tools.ServiceBus.Services;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;

namespace Azure.Mcp.Tools.ServiceBus.Commands.Topic;

public sealed class TopicDetailsCommand(ILogger<TopicDetailsCommand> logger) : SubscriptionCommand<BaseTopicOptions>
{
    private const string CommandTitle = "Get Service Bus Topic Details";
    private readonly ILogger<TopicDetailsCommand> _logger = logger;
    public override string Name => "details";

    public override string Description =>
        """
        Get details about a Service Bus topic. Returns topic properties and runtime information. Properties returned include
        number of subscriptions, max message size, max topic size, number of scheduled messages, etc.

        Required arguments:
        - namespace: The fully qualified Service Bus namespace host name. (This is usually in the form <namespace>.servicebus.windows.net)
        - topic: Topic name to get information about.
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new()
    {
        Destructive = false,
        Idempotent = true,
        OpenWorld = true,
        ReadOnly = true,
        LocalRequired = false,
        Secret = false
    };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.Options.Add(ServiceBusOptionDefinitions.Namespace);
        command.Options.Add(ServiceBusOptionDefinitions.Topic);
    }

    protected override BaseTopicOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.TopicName = parseResult.GetValueOrDefault<string>(ServiceBusOptionDefinitions.Topic.Name);
        options.Namespace = parseResult.GetValueOrDefault<string>(ServiceBusOptionDefinitions.Namespace.Name);
        return options;
    }

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        if (!Validate(parseResult.CommandResult, context.Response).IsValid)
        {
            return context.Response;
        }

        var options = BindOptions(parseResult);

        try
        {
            var service = context.GetService<IServiceBusService>();
            var details = await service.GetTopicDetails(
                options.Namespace!,
                options.TopicName!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(new(details), ServiceBusJsonContext.Default.TopicDetailsCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting Service Bus topic details");
            HandleException(context, ex);
        }

        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        ServiceBusException exception when exception.Reason == ServiceBusFailureReason.MessagingEntityNotFound =>
            $"Subscription not found. Please check the topic and subscription name and try again.",
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        ServiceBusException sbEx when sbEx.Reason == ServiceBusFailureReason.MessagingEntityNotFound => 404,
        _ => base.GetStatusCode(ex)
    };

    internal record TopicDetailsCommandResult(TopicDetails TopicDetails);
}
