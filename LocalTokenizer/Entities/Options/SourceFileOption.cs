using System;
using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalTokenizer.Entities.Options
{
    public class SourceFileOption<T> : BaseOption<T>
    {
        public SourceFileOption(string name, string description, 
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

        public static FileInfo ValidateFile(ArgumentResult result)
        {
            string filePath = result.Tokens.Single().Value;
            if (!File.Exists(filePath))
            {
                result.ErrorMessage = "File does not exist";
                return null;
            }

            return new FileInfo(filePath);
        }
    }
}
