using LocalTokenizer.Constants;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.CommandLine.Parsing;
using System.IO;
using System.Linq;

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
                result.ErrorMessage = "Template File does not exist.";
                return null;
            }

            var fileInfo = new FileInfo(filePath);

            // Checks valid extension (.json)
            if(fileInfo.Extension != ".json")
            {
                result.ErrorMessage = "Template File extension not supported. Only JSON files are supported.";
                return null;
            }

            // Checks if is a valid template
            JSchema schema = JSchema.Parse(TemplateFileConstants.schemaJson);
            string fileText = File.ReadAllText(filePath);
            JObject templateFile = JObject.Parse(fileText);

            if (!templateFile.IsValid(schema))
            {
                result.ErrorMessage = "This file is not compatible with a valid template.";
                return null;
            }

            return fileInfo;
        }
    }
}
