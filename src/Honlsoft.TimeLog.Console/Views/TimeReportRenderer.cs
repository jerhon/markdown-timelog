using Honlsoft.TimeLog;
using Honlsoft.TimeLog.Domain;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Honlsoft.TimeLog.Console.Views;

public class TimeReportRenderer {

    public Renderable[] Render(TimeReport summary) {

        var graph = new BarChart()
            .Width(60)
            .Label("[bold]Time Summary[/]")
            .CenterLabel()
            .AddItems(summary.Entries.OrderByDescending((s) => s.CalculateTotalTime()).Select((s) => new BarChartItem(s.Task, s.CalculateTotalTime().TotalHours)));

        var table = new Table();
        table.AddColumn("Duration");
        table.AddColumn("Task");
        table.AddColumn("Description");

        foreach (var summaryRecord in summary.Entries) {
            table.AddRow(summaryRecord.CalculateTotalTime().ToString(), summaryRecord.Task ?? "", summaryRecord.Records?.FirstOrDefault()?.Description ?? "");
            if (summaryRecord.Records.Length > 1) {
                foreach (var record in summaryRecord.Records.Skip(1)) {
                    table.AddRow("", "", record.Description);
                }
            }
        }
        return new Renderable[] { graph, table };
    }
}