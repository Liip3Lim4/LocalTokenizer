using System.CommandLine;
using System.CommandLine.Parsing;

namespace LocalTokenizer.Entities.Options;

public class BaseOption<T> : Option<T>
{
    public BaseOption(string name, string description, ParseArgument<T> parseArgument, string alias, bool isRequired) : base(name, parseArgument)
    {
        this.Description = description;
        this.AddAlias(alias);
        this.IsRequired = isRequired;
    }

    public BaseOption(string name, string description, string alias, bool isRequired, string[] validValues, T defaultValue) : base(name, description)
    {
        AddAlias(alias);
        IsRequired = isRequired;
        this.FromAmong(validValues);
        SetDefaultValue(defaultValue);
    }
}
