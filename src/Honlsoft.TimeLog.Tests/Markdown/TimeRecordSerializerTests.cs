using System;
using Xunit;

namespace Honlsoft.TimeLog.Markdown;

public class TimeRecordSerializerTests {


    [Fact]
    public void TimeRecordSerializer_AllFields() {
        TimeRecordSerializer serializer = new TimeRecordSerializer();

        var record = serializer.Deserialize("10:00 AM-12:00PM|1234|This is a task to do something");
        record.Should().NotBeNull();
        record.StartTime.Should().Be(new TimeOnly(10, 00, 00));
        record.EndTime.Should().Be(new TimeOnly(12, 00, 00));
        record.Task.Should().Be("1234");
        record.Description.Should().Be("This is a task to do something");
    }

    [Fact]
    public void TimeRecordSerializer_StartTime_Task_Description() {
        TimeRecordSerializer serializer = new TimeRecordSerializer();

        var record = serializer.Deserialize("10:00 AM|TASK|DESCRIPTION");
        record.Should().NotBeNull();
        record.StartTime.Should().Be(new TimeOnly(10, 00, 00));
        record.EndTime.Should().BeNull();
        record.Task.Should().Be("TASK");
        record.Description.Should().Be("DESCRIPTION");
    }


    [Fact]
    public void TimeRecordSerializer_HasTableSeperatorAtBeginningAndEnd() {
        TimeRecordSerializer serializer = new TimeRecordSerializer();

        var record = serializer.Deserialize("| 1:00 AM | TASK | DESCRIPTION |");
        record.Should().NotBeNull();
        record.StartTime.Should().Be(new TimeOnly(1, 00, 00));
        record.EndTime.Should().BeNull();
        record.Task.Should().Be("TASK");
        record.Description.Should().Be("DESCRIPTION");
    }

    [Fact]
    public void TimeRecordSerializer_StartTime_Description() {
        TimeRecordSerializer serializer = new TimeRecordSerializer();

        var record = serializer.Deserialize("10:00 AM|DESCRIPTION");
        record.Should().NotBeNull();
        record.StartTime.Should().Be(new TimeOnly(10, 00, 00));
        record.EndTime.Should().BeNull();
        record.Task.Should().BeNull();
        record.Description.Should().Be("DESCRIPTION");
    }


    [Fact]
    public void TimeRecordSerializer_StartTime_EmptyTask_EmptyDescription() {
        TimeRecordSerializer serializer = new TimeRecordSerializer();

        var record = serializer.Deserialize("|10:00 AM|||");
        record.Should().NotBeNull();
        record.StartTime.Should().Be(new TimeOnly(10, 00, 00));
        record.EndTime.Should().BeNull();
        record.Task.Should().BeNull();
        record.Description.Should().BeNull();
    }
}