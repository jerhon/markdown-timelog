using System;
using Xunit;

namespace Honlsoft.TimeLog.Domain;

public class TimeLogTests {


    [Fact]
    public void TimeLog_FillGaps_Success() {

        var record1 = new TimeEntry() {
            StartTime = new TimeOnly(4, 0, 0)
        };
        var record2 = new TimeEntry() {
            StartTime = new TimeOnly(5, 0, 0),
            EndTime = new TimeOnly(6, 0, 0)
        };
        var record3 = new TimeEntry() {
            StartTime = new TimeOnly(7, 0, 0)
        };

        TimeSheet sheet = new TimeSheet(new DateOnly(2021, 12, 28), new[] {
            record1,
            record2,
            record3
        });

        sheet.FillGaps();


        // should take the start time of the next record
        sheet.Records[0].EndTime.Should().Be(new TimeOnly(5, 0, 0));

        // time should not change since it was already given an end time
        sheet.Records[1].EndTime.Should().Be(new TimeOnly(6, 0, 0));

        // should be null, there is no later record to infer when the task started
        sheet.Records[2].EndTime.Should().BeNull();

    }
}