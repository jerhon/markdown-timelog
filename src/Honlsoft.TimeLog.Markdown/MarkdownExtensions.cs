using Microsoft.Extensions.DependencyInjection;

namespace Honlsoft.TimeLog.Markdown;

public static class MarkdownExtensions {
    public static void AddMarkdown(this IServiceCollection serviceCollection) {
        serviceCollection.AddSingleton<ITimeRecordRepository, MarkdownTimeRecordRepository>();
        serviceCollection.AddSingleton<TimeRecordSerializer>();
    }
}