namespace Honlsoft.TimeLog.Domain;

/// <summary>
/// A time record.
/// </summary>
public class TimeEntry {

    /// <summary>
    /// The time the entry started.
    /// </summary>
    public TimeOnly StartTime { get; set; }

    /// <summary>
    /// The time the entry ended.
    /// </summary>
    public TimeOnly? EndTime { get; set; }

    /// <summary>
    /// The identifying task number.
    /// </summary>
    public string? Task { get; set; }

    /// <summary>
    /// The description of the task.
    /// </summary>
    public string? Description { get; set; }


    /// <summary>
    /// Calculates the duration of the Time Record.  If the time record has no end, then it returns a zero duration.
    /// </summary>
    /// <returns>The duration as a TimeSpan.</returns>
    public TimeSpan CalculateDuration() {
        if (EndTime is not null) {
            return EndTime.Value - StartTime;
        }
        return TimeSpan.Zero;
    }

}