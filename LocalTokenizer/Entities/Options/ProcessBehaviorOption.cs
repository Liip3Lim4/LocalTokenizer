using System.CommandLine.Parsing;

namespace LocalTokenizer.Entities.Options;

public class ProcessBehaviorOption<T> : BaseOption<T>
{
    public ProcessBehaviorOption(string name, string description, ParseArgument<T> parseArgument, string alias, bool isRequired) 
        : base(name, description, parseArgument, alias, isRequired)
    {
    }

    public ProcessBehaviorOption(string name, string description, string alias, bool isRequired, string[] validValues, T defaultValue)
        : base(name, description, alias, isRequired, validValues, defaultValue)
    {
    }
}
