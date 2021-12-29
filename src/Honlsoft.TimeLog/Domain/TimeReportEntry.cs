
namespace Honlsoft.TimeLog.Domain;

/// <summary>
/// The time record summary to use.
/// </summary>
public class TimeReportEntry {


    /// <summary>
    /// The task.
    /// </summary>
    public string Task { get; set; } = "";

    /// <summary>
    /// The list of records.
    /// </summary>
    public TimeEntry[] Records { get; set; } = Array.Empty<TimeEntry>();

    /// <summary>
    /// Calculates the total time in the time summary.
    /// </summary>
    /// <returns>The total time of the summarized entries..</returns>
    public TimeSpan CalculateTotalTime() {
        return Records.Aggregate(TimeSpan.Zero, (TimeSpan pv, TimeEntry cv) => pv + cv.CalculateDuration());
    }
}