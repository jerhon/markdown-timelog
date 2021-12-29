using Honlsoft.TimeLog;
using Honlsoft.TimeLog.Domain;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Honlsoft.TimeLog.Console.Views;

public class TimeSheetRenderer {

    public Renderable Render(TimeSheet log) {
        var table = new Table();
        table.AddColumn("Start");
        table.AddColumn("End");
        table.AddColumn("Task ID");
        table.AddColumn("Description", (tc) => {
             tc.Width = 40;
        });
        foreach (var row in log.Records) {
            table.AddRow(row.StartTime.ToString(), row.EndTime?.ToString() ?? "", row.Task ?? "", row.Description ?? "");
        }
        return table;
    }
}