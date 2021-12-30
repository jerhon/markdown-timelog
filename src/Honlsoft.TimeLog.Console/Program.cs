using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Help;
using System.CommandLine.Parsing;
using Honlsoft.TimeLog.Console;
using Honlsoft.TimeLog;
using Honlsoft.TimeLog.Markdown;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

// Setup the DI container
ServiceCollection collection = new ServiceCollection();
collection.AddConsole();
collection.AddDomain();
collection.AddMarkdown();

var serviceProvider = collection.BuildServiceProvider();


// Setup the Command Line Parser
var commands = serviceProvider.GetServices<ICommandFactory>();
var rootCommand = new RootCommand();
rootCommand.Description = "Time Log Helper for Markdown";

// Register each command
foreach (var command in commands) {
    rootCommand.AddCommand(command.BuildCommand());
}


// We'll add fancy figlet text to the root of the application
var commandLine = new CommandLineBuilder(rootCommand)
    .UseDefaults()
    .UseHelp((ctx) => HelpBuilder.Default.GetLayout().Skip(1).Prepend(_ => Spectre.Console.AnsiConsole.Write(new FigletText("Honlsoft Time Log"))))
    .Build();

await commandLine.InvokeAsync(args);
