using LocalTokenizer.CommandHandlers;
using System.CommandLine;
using System.Threading.Tasks;

namespace LocalTokenizer;
public class Program
{
    public static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("LocalTokenizer - Easing the stress of adapting tokenized files into your life");

        // Initialize all options and UntokenizeFileCommand parameters
        _ = new UntokenizeFileCommandHandler(rootCommand);

        //return await rootCommand.InvokeAsync("utk -sf teste_original.yaml -tf template.json -env dev -pb ind");
        if (args.Length == 0)
        {
            return await rootCommand.InvokeAsync("--help");
        }

        return await rootCommand.InvokeAsync(args);

    }
}
