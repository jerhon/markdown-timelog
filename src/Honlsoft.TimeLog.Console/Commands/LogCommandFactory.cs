using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using Honlsoft.TimeLog.Console.Views;
using Honlsoft.TimeLog.Domain;
using Honlsoft.TimeLog.UseCases;
using Honlsoft.TimeLog.UseCases.Logs;
using MediatR;
using Spectre.Console;


namespace Honlsoft.TimeLog.Console.Commands;

public class LogCommandFactory: ICommandFactory {

    private readonly IMediator _mediator;
    private readonly IRenderer<TimeSheet> _timeSheetRenderer;
    private readonly IRenderer<TimeReport> _timeReportRenderer;

    public LogCommandFactory(IMediator mediator, IRenderer<TimeSheet> timeSheetRenderer, IRenderer<TimeReport> timeReportRenderer) {
        _mediator = mediator;
        _timeSheetRenderer = timeSheetRenderer;
        _timeReportRenderer = timeReportRenderer;
    }

    public Command BuildCommand() {
        var command = new Command("logs");
        command.Add(GetCommand());
        command.Add(SummaryCommand());
        command.Description = "Operations for time logs in markdown files, by default will look for a markdown file with the current date in the filename such as 2021-12-28.";
        return command;
    }

    /// <summary>
    /// Implements the 'log get' command.
    /// </summary>
    /// <returns>The command for the 'log get' command.</returns>
    private Command GetCommand() {
        var command = new Command("get");
        AddCommonOptions(command);
        command.Description = "Get and display time logs in a markdown file.";
        command.Handler = CommandHandler.Create((DateOnlyWrapper date) => {
            var result = _mediator.Send(new GetTimeRecords(){ Date = date.Value }).Result;
            _timeSheetRenderer.Render(result.TimeSheet);
            return 0;
        });
        return command;
    }


    private Command SummaryCommand() {
        var command = new Command("summary");
        
        // Note: It appears the application doesn't convert the DateOnly struct properly, so we will treat it as a string and parse in the command handler
        AddCommonOptions(command);
        command.Description = "Get and display time summary by task.";
        command.Handler = CommandHandler.Create((DateOnlyWrapper date) => {
            var result = _mediator.Send(new SummarizeTimeRecords() { Date = date.Value }).Result;
            _timeReportRenderer.Render(result.Report);
        });
        return command;
    }

    private void AddCommonOptions(Command command) {
        command.AddOption(new Option<DateOnlyWrapper>("--date", () => new DateOnlyWrapper(DateOnly.FromDateTime(DateTime.Now)), "The date to evaluate the logs on."));
    }
}
