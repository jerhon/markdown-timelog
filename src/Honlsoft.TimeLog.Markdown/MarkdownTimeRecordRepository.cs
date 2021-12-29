using System.Text.RegularExpressions;
using Honlsoft.TimeLog;
using Honlsoft.TimeLog.Domain;

namespace Honlsoft.TimeLog.Markdown;

public class MarkdownTimeRecordRepository : ITimeRecordRepository {

    private readonly IMarkdownParserOptions _options;
    private readonly TimeRecordSerializer _serializer;


    /// <summary>
    /// Creates a markdown parser.
    /// </summary>
    /// <param name="options">The base options used to </param>
    public MarkdownTimeRecordRepository(IMarkdownParserOptions options, TimeRecordSerializer serializer) {
        _options = options;
        _serializer = serializer;
    }

    public async Task<TimeSheet> LookupTimeSheetAsync(DateOnly date) {

        var dateMatch = date.ToString("yyyy-MM-dd");
        var files = Directory.EnumerateFiles(_options.RootDirectory, "*.md");
        var matchingEntries = files.Where((f) => f.Contains(dateMatch));
        var results = await Task.WhenAll(matchingEntries.Select(ParseRecordsFromFileAsync));

        return new TimeSheet(date, results.SelectMany((s) => s).ToArray());
    }

    /// <summary>
    /// Parses records from a file.
    /// </summary>
    /// <param name="fileName">The filename to parse.</param>
    /// <returns>The list of time records.</returns>
    public async Task<TimeEntry[]> ParseRecordsFromFileAsync(string fileName) {
        using Stream file = File.OpenRead(fileName);
        using StreamReader textReader = new(file);
        List<TimeEntry> records = new();

        string? line = await textReader.ReadLineAsync();
        while (line != null) {

            TimeEntry? record = _serializer.Deserialize(line);
            if (record != null) {
                records.Add(record);
            }

            line = await textReader.ReadLineAsync();
        }

        return records.ToArray();
    }

}