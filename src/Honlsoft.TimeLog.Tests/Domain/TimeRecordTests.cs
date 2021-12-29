using System;
using Xunit;

namespace Honlsoft.TimeLog.Domain;

public class TimeRecordTests {

    [Fact]
    public void TimeRecord_CalculateDuration_Success() {
        TimeEntry entry = new TimeEntry() {
            StartTime = new TimeOnly(1, 0, 0),
            EndTime = new TimeOnly(5, 30, 0),
        };

        entry.CalculateDuration()
            .Should()
            .Be(new TimeSpan(0, 4, 30, 0));
    }

    [Fact]
    public void TimeRecord_CalculateDuration_NoEndTime_Returns0() {
        TimeEntry entry = new TimeEntry() {
            StartTime = new TimeOnly(1, 0, 0),
        };

        entry.CalculateDuration()
            .Should()
            .Be(TimeSpan.Zero);
    }
}