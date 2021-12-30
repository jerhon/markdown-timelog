namespace Honlsoft.TimeLog.Console.Commands; 


/// <summary>
/// Wraps a DateOnly to have a string constructor.  This allows the wrapper type to be used directly in the System.CommandLine library.
/// </summary>
public class DateOnlyWrapper {
    /// <summary>
    /// Parses a date.
    /// </summary>
    /// <param name="date">The date to parse.</param>
    public DateOnlyWrapper(string date) {
        this.Value = DateOnly.Parse(date);
    }

    /// <summary>
    /// The Date to assign.
    /// </summary>
    /// <param name="value">The date to assign.</param>
    public DateOnlyWrapper(DateOnly value) {
        Value = value;
    }
    
    /// <summary>
    /// The date.
    /// </summary>
    public DateOnly Value { get;  }
}