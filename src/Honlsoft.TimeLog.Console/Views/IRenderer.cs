namespace Honlsoft.TimeLog.Console.Views; 

/// <summary>
/// Renders the 
/// </summary>
/// <typeparam name="TType"></typeparam>
public interface IRenderer<TType> {

    void Render(TType report);
}