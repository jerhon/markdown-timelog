using Honlsoft.TimeLog.UseCases;

namespace Honlsoft.TimeLog.Domain;

public class TimeSheet {

    public TimeSheet(DateOnly date, TimeEntry[] records) {
        Date = date;
        Records = records;
    }
    
    /// <summary>
    /// The date of the time entry.
    /// </summary>
    public DateOnly Date { get; private set; }

    /// <summary>
    /// The records for the Time entries
    /// </summary>
    public TimeEntry[] Records { get; private set; }

    /// <summary>
    /// Fills any gaps in the time log.  Updates a time entry it it doesn't have a specific end time, it will take the start time of the next task as it's end time.
    /// </summary>
    public void FillGaps() {
        var sortedRecords = Records.OrderBy((r) => r.StartTime);
        TimeEntry? previousRecord = null;
        foreach (var record in sortedRecords) {
            if (previousRecord != null) {
                previousRecord.EndTime ??= record.StartTime;
            }
            previousRecord = record;
        }
    }

    /// <summary>
    /// Summarizes the time records.
    /// </summary>
    /// <returns>A time report, with records grouped by task.</returns>
    public TimeReport SummarizeByTask() {

        var summaries = new List<TimeReportEntry>();
        var records = this.Records.GroupBy((r) => r.Task);
        foreach (var record in records) {
            if (string.IsNullOrEmpty(record.Key)) {
                foreach (var singleRecord in record) {
                    summaries.Add(new TimeReportEntry() {
                            Records = new[] { singleRecord },
                            Task = record?.Key,
                    });
                }
            } else {
                summaries.Add(new TimeReportEntry() {
                    Records = record.ToArray(),
                    Task = record?.Key,
                });
            }
        }

        return new TimeReport(this.Date, summaries.ToArray());
    }
}