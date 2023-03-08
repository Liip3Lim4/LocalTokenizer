using System.CommandLine;

namespace LocalTokenizer.Entities.Commands
{
    public class BaseCommand : Command
    {
        public BaseCommand(string name, string description) : base(name, description)
        {
        }

        public BaseCommand(string name, string description, string alias) : base(name, description)
        {
            AddAlias(alias);
        }

    }
}
