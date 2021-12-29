using System.CommandLine;

namespace Honlsoft.TimeLog.Console;

public interface ICommandFactory {

    Command BuildCommand();
}