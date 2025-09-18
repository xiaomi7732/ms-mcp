// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Mcp.Core.Commands;
using Azure.Mcp.Core.Extensions;
using Azure.Mcp.Tools.MySql.Options;
using Azure.Mcp.Tools.MySql.Options.Server;
using Azure.Mcp.Tools.MySql.Services;
using Microsoft.Extensions.Logging;

namespace Azure.Mcp.Tools.MySql.Commands.Server;

public sealed class ServerParamGetCommand(ILogger<ServerParamGetCommand> logger) : BaseServerCommand<ServerParamGetOptions>(logger)
{
    private const string CommandTitle = "Get MySQL Server Parameter";

    public override string Name => "param";

    public override string Description => "Retrieves the current value of a single server configuration parameter on an Azure Database for MySQL Flexible Server. Use to inspect a setting (e.g. max_connections, wait_timeout, slow_query_log) before changing it.";

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
        command.Options.Add(MySqlOptionDefinitions.Param);
    }

    protected override ServerParamGetOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Param = parseResult.GetValueOrDefault<string>(MySqlOptionDefinitions.Param.Name);
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
            IMySqlService mysqlService = context.GetService<IMySqlService>() ?? throw new InvalidOperationException("MySQL service is not available.");
            string paramValue = await mysqlService.GetServerParameterAsync(options.Subscription!, options.ResourceGroup!, options.User!, options.Server!, options.Param!);
            context.Response.Results = !string.IsNullOrEmpty(paramValue) ?
                ResponseResult.Create(new(options.Param!, paramValue), MySqlJsonContext.Default.ServerParamGetCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred retrieving server parameter.");
            HandleException(context, ex);
        }
        return context.Response;
    }

    internal record ServerParamGetCommandResult(string Parameter, string Value);
}
