using System.Text.RegularExpressions;
using Honlsoft.TimeLog.Domain;

namespace Honlsoft.TimeLog.Markdown;

public class TimeRecordSerializer {

    private const char Seperator = '|';
    private static readonly Regex _lineRegex = new Regex(@"^[\s|]*(?<startTime>[0-9]{1,2}:[0-9]{1,2}\s*(AM|PM|am|pm)){1,2}\s*(-\s*(?<endTime>[0-9]{1,2}:[0-9]{1,2}\s*(AM|PM|am|pm)))?\s*(?<ending>|.*)$");

    /// <summary>
    /// Tries to parse a time record from a line of text.
    /// </summary>
    /// <param name="line">The line to parse.</param>
    /// <returns>The time record, otherwise null if no time record is found.</returns>
    public TimeEntry? Deserialize(string line) {
        var match = _lineRegex.Match(line);
        if (match.Success) {
            TimeOnly startTime;
            TimeOnly? endTime = null;
            string? task = null;
            string? description = null;

            var rawStartTime = match.Groups["startTime"].Value;
            var rawEndTime = match.Groups["endTime"].Value;

            if (!string.IsNullOrWhiteSpace(rawStartTime)) {
                startTime = TimeOnly.Parse(rawStartTime);
            }
            if (!string.IsNullOrWhiteSpace(rawEndTime)) {
                endTime = TimeOnly.Parse(rawEndTime);
            }
            if (match.Groups.ContainsKey("ending")) {
                var ending = match.Groups["ending"].Value;
                var parts = ending.TrimStart(Seperator).Split(Seperator);
                if (parts.Length > 1) {
                    task = parts[0];
                    description = ending.Substring(task.Length + 1);
                } else {
                    description = ending;
                }

                if (task != null) {
                    task = task.TrimStart(Seperator).Trim();
                }
                if (description != null) {
                    description = description.Trim(Seperator).Trim();
                }
            }

            if (task == String.Empty) {
                task = null;
            }

            if (description == String.Empty) {
                description = null;
            }

            return new TimeEntry { Description = description, Task = task, EndTime = endTime, StartTime = startTime };
        }
        return null;
    }

}