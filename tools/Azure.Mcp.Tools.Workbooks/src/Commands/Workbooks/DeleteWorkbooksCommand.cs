// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Mcp.Core.Commands;
using Azure.Mcp.Core.Extensions;
using Azure.Mcp.Tools.Workbooks.Options;
using Azure.Mcp.Tools.Workbooks.Options.Workbook;
using Azure.Mcp.Tools.Workbooks.Services;
using Microsoft.Extensions.Logging;

namespace Azure.Mcp.Tools.Workbooks.Commands.Workbooks;

public sealed class DeleteWorkbooksCommand(ILogger<DeleteWorkbooksCommand> logger) : GlobalCommand<DeleteWorkbookOptions>
{
    private const string CommandTitle = "Delete Workbook";
    private readonly ILogger<DeleteWorkbooksCommand> _logger = logger;

    public override string Name => "delete";

    public override string Description =>
        """
        Delete a workbook by its Azure resource ID.
        This command soft deletes the workbook: it will be retained for 90 days.
        If needed, you can restore it from the Recycle Bin through the Azure Portal.

        To learn more, visit: https://learn.microsoft.com/azure/azure-monitor/visualize/workbooks-manage
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new()
    {
        Destructive = true,
        Idempotent = true,
        OpenWorld = true,
        ReadOnly = false,
        LocalRequired = false,
        Secret = false
    };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.Options.Add(WorkbooksOptionDefinitions.WorkbookId);
    }

    protected override DeleteWorkbookOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.WorkbookId = parseResult.GetValueOrDefault<string>(WorkbooksOptionDefinitions.WorkbookId.Name);
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
            var workbooksService = context.GetService<IWorkbooksService>();
            var deleted = await workbooksService.DeleteWorkbook(options.WorkbookId!, options.RetryPolicy, options.Tenant);

            if (deleted)
            {
                context.Response.Results = ResponseResult.Create(new(options.WorkbookId!, "Successfully deleted"),
                    WorkbooksJsonContext.Default.DeleteWorkbooksCommandResult);
            }
            else
            {
                throw new InvalidOperationException($"Failed to delete workbook with ID '{options.WorkbookId}'");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting workbook with ID: {WorkbookId}", options.WorkbookId);
            HandleException(context, ex);
        }

        return context.Response;
    }

    public sealed record DeleteWorkbooksCommandResult(string WorkbookId, string Message);
}
