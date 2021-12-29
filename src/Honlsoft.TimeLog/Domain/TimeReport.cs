namespace Honlsoft.TimeLog.Domain;

/// <summary>
/// Represents a summary of time records.  Summaries are determined by the Task on the Time Record.
/// </summary>
public class TimeReport {

    public TimeReport(DateOnly date, TimeReportEntry[] reportEntries) {
        this.Entries = reportEntries;
    }
    
    /// <summary>
    /// The date of the time summary.
    /// </summary>
    public DateOnly Date { get; private set; }

    /// <summary>
    /// The summaries for time records grouped by task.
    /// </summary>
    public TimeReportEntry[] Entries { get; private set; }
}