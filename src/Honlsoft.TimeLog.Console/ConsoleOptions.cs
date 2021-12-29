
using Honlsoft.TimeLog.Markdown;

namespace Honlsoft.TimeLog.Console;

public class ConsoleOptions : IMarkdownParserOptions {

    public string RootDirectory { get; } = System.Environment.CurrentDirectory;

}