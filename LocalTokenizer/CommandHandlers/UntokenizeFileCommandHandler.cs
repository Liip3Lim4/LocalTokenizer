using LocalTokenizer.Constants;
using LocalTokenizer.Entities.Commands;
using LocalTokenizer.Entities.Options;
using System;
using System.CommandLine;
using System.IO;
using System.Linq;

namespace LocalTokenizer.CommandHandlers
{
    public class UntokenizeFileCommandHandler
    {
        private readonly UntokenizeFileCommand _untokenizeFileCommand;
        private readonly SourceFileOption<FileInfo> _sourceFileOption;
        private readonly TemplateFileOption<FileInfo> _templateFileOption;
        private readonly EnvironmentOption<string> _envOption;
        private readonly ProcessBehaviorOption<string> _processBehaviorOption;

        public UntokenizeFileCommandHandler(RootCommand rc)
        {
            _untokenizeFileCommand = new UntokenizeFileCommand("untokenize", "Read and replace all tokens in a specific file.", "utk");

            // Instantiate all needed options
            // SourceFile Option
            _sourceFileOption = new SourceFileOption<FileInfo>(
                name: "--source-file",
                description: "set the file to untokenize.",
                parseArgument: result => SourceFileOption<FileInfo>.ValidateFile(result),
                "-sf",
                true
                );
            // TemplateFile Option
            _templateFileOption = new TemplateFileOption<FileInfo>(
                name: "--template-file",
                description: "set the JSON template file to be used on untokenize.",
                parseArgument: result => TemplateFileOption<FileInfo>.ValidateFile(result),
                "-tf",
                true
                );
            // Enviroment Option
            _envOption = new EnvironmentOption<string>(
                name: "--environment",
                description: "set environment name used in JSON template file. Default value is first finded environment.",
                parseArgument: result => result.Tokens.Single().Value,
                "-env",
                false
                );
            // ProcessBehavior Option
            _processBehaviorOption = new ProcessBehaviorOption<string>(
                name: "--process-behavior",
                description: "set a behavior to apply when an unmapped token/value be found.",
                "-pb",
                false,
                ProcessBehaviorsConstants.ValidValues,
                ProcessBehaviorsConstants.StrictBehavior
                );

            // Associate all options
            _untokenizeFileCommand.AddOption(_sourceFileOption);
            _untokenizeFileCommand.AddOption(_templateFileOption);
            _untokenizeFileCommand.AddOption(_envOption);
            _untokenizeFileCommand.AddOption(_processBehaviorOption);

            // Config all option handlers
            _untokenizeFileCommand.SetHandler((file, template, environment, processBehavior) =>
            {
                _untokenizeFileCommand.ReadTemplate(template, environment);
                _untokenizeFileCommand.ReadFile(file, processBehavior);
            }, _sourceFileOption, _templateFileOption, _envOption, _processBehaviorOption);

            rc.AddCommand(_untokenizeFileCommand);
        }
    }
}
