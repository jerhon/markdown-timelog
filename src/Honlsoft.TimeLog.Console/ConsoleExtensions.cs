using Honlsoft.TimeLog.Console.Commands;
using Honlsoft.TimeLog.Console.Views;
using Honlsoft.TimeLog;
using Honlsoft.TimeLog.Domain;
using Honlsoft.TimeLog.Markdown;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace Honlsoft.TimeLog.Console;

public static class ConsoleExtensions {

    public static void AddConsole(this IServiceCollection collection) {
        collection.AddSingleton<IRenderer<TimeSheet>, TimeSheetRenderer>();
        collection.AddSingleton<IRenderer<TimeReport>, TimeReportRenderer>();
        collection.AddSingleton<ICommandFactory, LogCommandFactory>();
        collection.AddSingleton<IMarkdownParserOptions, ConsoleOptions>();
        collection.AddSingleton<IAnsiConsole>(AnsiConsole.Console);
    }
}