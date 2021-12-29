using System.CommandLine;
using Honlsoft.TimeLog.Console;
using Honlsoft.TimeLog;
using Honlsoft.TimeLog.Markdown;
using Microsoft.Extensions.DependencyInjection;

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


// Execute the command from the command line.
await rootCommand.InvokeAsync(args);