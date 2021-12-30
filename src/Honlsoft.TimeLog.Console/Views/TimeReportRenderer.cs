using Honlsoft.TimeLog;
using Honlsoft.TimeLog.Domain;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Honlsoft.TimeLog.Console.Views;

public class TimeReportRenderer: IRenderer<TimeReport> {

    private IAnsiConsole _console;
    
    public TimeReportRenderer(IAnsiConsole console) {
        _console = console;
    }
    
    public void Render(TimeReport report) {
        if (report == null || report.Entries.Length == 0) {
            return;
        }
        
        var graph = new BarChart()
            .Width(60)
            .Label("[bold]Time Summary[/]")
            .CenterLabel()
            .AddItems(report.Entries.OrderByDescending((s) => s.CalculateTotalTime()).Select((s) => new BarChartItem(s.Task, s.CalculateTotalTime().TotalHours)));
        
        _console.Write(graph);

        var table = new Table();
        table.AddColumn("Duration");
        table.AddColumn("Task");
        table.AddColumn("Description");

        foreach (var summaryRecord in report.Entries) {
            table.AddRow(summaryRecord.CalculateTotalTime().ToString(), summaryRecord.Task ?? "", summaryRecord.Records?.FirstOrDefault()?.Description ?? "");
            if (summaryRecord.Records.Length > 1) {
                foreach (var record in summaryRecord.Records.Skip(1)) {
                    table.AddRow("", "", record.Description);
                }
            }
        }
        
        _console.Write(table);
    }
}