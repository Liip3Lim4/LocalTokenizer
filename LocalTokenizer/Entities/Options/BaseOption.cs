using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.CommandLine.Help.HelpBuilder;

namespace LocalTokenizer.Entities.Options
{
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
}
