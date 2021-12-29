using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using Honlsoft.TimeLog.Console.Views;
using Honlsoft.TimeLog.UseCases;
using Honlsoft.TimeLog.UseCases.Logs;
using MediatR;
using Spectre.Console;


namespace Honlsoft.TimeLog.Console.Commands;

public class LogCommandFactory: ICommandFactory {

    private readonly IMediator _mediator;
    private readonly TimeSheetRenderer _renderer;
    private readonly TimeReportRenderer _reportRenderer;

    public LogCommandFactory(IMediator mediator, TimeSheetRenderer renderer, TimeReportRenderer reportRenderer) {
        _mediator = mediator;
        _renderer = renderer;
        _reportRenderer = reportRenderer;
    }

    public Command BuildCommand() {
        var command = new Command("logs");
        command.Add(GetCommand());
        command.Add(SummaryCommand());
        command.Description = "Operations for time logs in markdown files, by default will look for a markdown file with the current date in the filename such as 2021-12-28.";
        command.AddOption(new Option("date", "The date to evaluate the logs on.", typeof(DateOnly), () => DateOnly.FromDateTime(DateTime.Now)));
        return command;
    }

    /// <summary>
    /// Implements the 'log get' command.
    /// </summary>
    /// <returns>The command for the 'log get' command.</returns>
    private Command GetCommand() {
        var command = new Command("get");
        command.Description = "Get and display time logs in a markdown file.";
        command.Handler = CommandHandler.Create(() => {
            var result = _mediator.Send(new GetTimeRecords()).Result;
            AnsiConsole.Write(_renderer.Render(result.TimeSheet));
            return 0;
        });
        return command;
    }


    private Command SummaryCommand() {
        var command = new Command("summary");
        command.Description = "Get and display time summary by task.";
        command.Handler = CommandHandler.Create(() => {
            var result = _mediator.Send(new SummarizeTimeRecords()).Result;
            var renderings = _reportRenderer.Render(result.Report);
            foreach (var render in renderings) {
                AnsiConsole.Write(render);
            }
            return 0;
        });
        return command;
    }
}
