using Honlsoft.TimeLog;
using Honlsoft.TimeLog.Domain;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Honlsoft.TimeLog.Console.Views;

public class TimeSheetRenderer: IRenderer<TimeSheet> {
    private readonly IAnsiConsole _console;

    public TimeSheetRenderer(IAnsiConsole console) {
        _console = console;
    }

    public void Render(TimeSheet report) {
        if (report == null || report.Records.Length == 0) {
            return;
        }
        
        var table = new Table();
        table.AddColumn("Start");
        table.AddColumn("End");
        table.AddColumn("Task ID");
        table.AddColumn("Description", (tc) => {
             tc.Width = 40;
        });
        foreach (var row in report.Records) {
            table.AddRow(row.StartTime.ToString(), row.EndTime?.ToString() ?? "", row.Task ?? "", row.Description ?? "");
        }
        
        _console.Write(table);
    }
}