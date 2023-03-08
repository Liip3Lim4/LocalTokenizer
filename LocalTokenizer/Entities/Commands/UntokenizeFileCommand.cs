using LocalTokenizer.Constants;
using LocalTokenizer.Constants.Messages;
using LocalTokenizer.Entities.Models;
using LocalTokenizer.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LocalTokenizer.Entities.Commands;

public class UntokenizeFileCommand : BaseCommand
{
    private List<EnvironmentPropertyToken> TemplateTokens { get; set; }

    public UntokenizeFileCommand(string name, string description) : base(name, description)
    {
    }

    public UntokenizeFileCommand(string name, string description, string alias) : base(name, description, alias)
    {
        
    }

    public void ReadFile(FileInfo file, string processBehavior = ProcessBehaviorsConstants.StrictBehavior)
    {
        string pattern = @"___(.*?)___";
        Regex rg = new(pattern);

        List<string> lines = new();

        File.ReadLines(file.FullName).ToList()
            .ForEach(line =>
            {
                string untokenizedLine = line;
                int occurrenceCount = 0;

                MatchCollection matches = rg.Matches(line);
                foreach (Match match in matches.Cast<Match>())
                {
                    occurrenceCount += 1;

                    string tokenName = string.Concat("___", match.Groups[1].Value, "___");                        
                    string tokenValue = TemplateTokens.Where(tk => tk.TokenName == tokenName).FirstOrDefault()?.Value;
                    
                    if (string.IsNullOrEmpty(tokenValue))
                    {
                        if (ProcessBehaviorsConstants.IsStrictBehavior(processBehavior))
                        {
                            MessageManager.SendErrorMessage($"An unmapped token was found. Check your template. Token: {tokenName}", true);
                        }

                        continue;
                    }

                    // Controls number of occurrence to treat more then one token in the same line
                    untokenizedLine = occurrenceCount == 1 ? line.Replace(tokenName, tokenValue) : untokenizedLine.Replace(tokenName, tokenValue);                        
                }

                lines.Add(untokenizedLine);
            });

        string[] fileDetails = file.Name.Split(".");
        string fileName = string.Concat(file.DirectoryName, "\\", fileDetails[0], "_untokenized.", fileDetails[1]);

        // Check if file already exists. If yes, delete it.
        try
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Create a new file
            using (StreamWriter sw = File.CreateText(fileName))
            {
                foreach (var line in lines)
                {
                    sw.WriteLine(line);
                }

            }

            MessageManager.SendSuccessMessage("File untokenized with success.", false);
            MessageManager.SendCustomMessage(
                new List<CustomMessageFragment>{ 
                    new CustomMessageFragment("Path: ", ConsoleColor.DarkYellow, false),
                    new CustomMessageFragment(fileName, false)
                },              
                false
            );
        }
        catch(Exception ex)
        {
            MessageManager.SendErrorMessage($"An error occurred. Error: {ex.ToString}", true);
        }
        
    }

    public void ReadTemplate(FileInfo file, string environment = null)
    {
        var jsonFile = File.ReadAllText(file.FullName);
        Template fileTemplate = JsonConvert.DeserializeObject<Template>(jsonFile);

        EnvironmentProperty envProps = string.IsNullOrEmpty(environment) 
            ? fileTemplate.Env.FirstOrDefault() 
            : fileTemplate.Env.Where(ep => ep.EnvName == environment).FirstOrDefault();

        if (envProps == null)
        {
            MessageManager.SendErrorMessage(
                "The environment value passed to be used in template file was not found.",
                true);
        }

        TemplateTokens = new List<EnvironmentPropertyToken>();
        TemplateTokens.AddRange(envProps.Tokens.ToList());
    }
}
