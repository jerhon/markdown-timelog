using Honlsoft.TimeLog.Domain;

namespace Honlsoft.TimeLog;

public interface ITimeRecordRepository {

    /// <summary>
    /// Returns a list of time records for the given day.
    /// </summary>
    /// <param name="date">The date to lookup the time records for.</param>
    /// <returns>A list of time records.</returns>
    Task<TimeSheet> LookupTimeSheetAsync(DateOnly date);

}