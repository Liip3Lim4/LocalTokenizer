using System.CommandLine.Parsing;

namespace LocalTokenizer.Entities.Options;

public class EnvironmentOption<T> : BaseOption<T>
{
    public EnvironmentOption(string name, string description, 
        ParseArgument<T> parseArgument, 
        string alias, 
        bool isRequired) : base(
            name, 
            description, 
            parseArgument, 
            alias, 
            isRequired)
    {
    }
}
