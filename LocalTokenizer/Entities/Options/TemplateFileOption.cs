using System;
using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalTokenizer.Entities.Options
{
    public class TemplateFileOption<T> : BaseOption<T>
    {
        public TemplateFileOption(string name, string description, 
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

            //Checks if exists
            if (!File.Exists(filePath))
            {
                result.ErrorMessage = "Template File does not exist";
                return null;
            }
            var fileInfo = new FileInfo(filePath);
            // Checks valid extension (.json)
            if(fileInfo.Extension != ".json")
            {
                result.ErrorMessage = "Template File extension not supported. Only JSON files supported";
                return null;
            }

            return fileInfo;
        }
    }
}
