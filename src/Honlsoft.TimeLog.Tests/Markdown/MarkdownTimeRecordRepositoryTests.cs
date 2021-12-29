using System;
using System.IO;
using System.Threading.Tasks;
using Honlsoft.TimeLog.Domain;
using Xunit;

namespace Honlsoft.TimeLog.Markdown;

public class MarkdownTimeRecordRepositoryTests {


    private IMarkdownParserOptions _options;

    public MarkdownTimeRecordRepositoryTests() {
        var testFiles = Path.Join(TestUtils.GetDirectory(), "TestFiles");
        var mock = new Mock<IMarkdownParserOptions>();
        mock.SetupGet((m) => m.RootDirectory).Returns(testFiles);
        _options = mock.Object;
    }

    [Fact]
    public async Task MarkdownTimeRecordRepository_LookupRecords_Success() {


        MarkdownTimeRecordRepository timeRecordRepository = new MarkdownTimeRecordRepository(_options, new TimeRecordSerializer());
        var timeSheet = await timeRecordRepository.LookupTimeSheetAsync(DateOnly.Parse("2021-12-28"));
        timeSheet.Date.Should().Be(DateOnly.Parse("2021-12-28"));
        timeSheet.Records.Should().HaveCount(1);

        //| 1:00AM-1:00PM | Task1234 | It's a long day |
        timeSheet.Records[0].Should().BeEquivalentTo(new TimeEntry() {
            StartTime = TimeOnly.Parse("1:00AM"),
            EndTime = TimeOnly.Parse("1:00PM"),
            Description = "It's a long day",
            Task = "Task1234"
        });
    }

    [Fact]
    public async Task MarkdownTimeRecordRepository_LookupRecords_MultipleEntries_Success() {

        var timeRecordRepository = new MarkdownTimeRecordRepository(_options, new TimeRecordSerializer());
        var timeSheet = await timeRecordRepository.LookupTimeSheetAsync(DateOnly.Parse("2021-12-29"));
        timeSheet.Records.Should().HaveCount(3);

        /*
            | Time          | Task      | Description              |
            |---------------|-----------|--------------------------|
            | 1:00PM        | Task1234  | complete some task stuff |
            | 2:00PM        | Task15234 |  |
            | 3:00PM-4:00PM | | complete some task stuff 2 |
         */

        timeSheet.Records[0].Should().BeEquivalentTo(new TimeEntry() {
            StartTime = TimeOnly.Parse("1:00PM"),
            EndTime = null,
            Description = "complete some task stuff",
            Task = "Task1234"
        });

        timeSheet.Records[1].Should().BeEquivalentTo(new TimeEntry() {
            StartTime = TimeOnly.Parse("2:00PM"),
            EndTime = null,
            Description = null,
            Task = "Task15234"
        });

        timeSheet.Records[2].Should().BeEquivalentTo(new TimeEntry() {
            StartTime = TimeOnly.Parse("3:00PM"),
            EndTime = TimeOnly.Parse("4:00PM"),
            Description = "complete some task stuff 2",
            Task = null
        });

    }
}