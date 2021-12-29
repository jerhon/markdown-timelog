using Honlsoft.TimeLog.Console.Commands;
using Honlsoft.TimeLog.Console.Views;
using Honlsoft.TimeLog;
using Honlsoft.TimeLog.Markdown;
using Microsoft.Extensions.DependencyInjection;

namespace Honlsoft.TimeLog.Console;

public static class ConsoleExtensions {

    public static void AddConsole(this IServiceCollection collection) {
        collection.AddSingleton<TimeSheetRenderer>();
        collection.AddSingleton<TimeReportRenderer>();
        collection.AddTransient<ICommandFactory, LogCommandFactory>();
        collection.AddSingleton<IMarkdownParserOptions, ConsoleOptions>();
    }
}