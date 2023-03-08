using System;

namespace LocalTokenizer.Utils;

public static class MessageManager
{
    public static void SendErrorMessage(string message, bool finishApplication)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;

        Console.Error.WriteLine(message);

        Console.ForegroundColor = originalColor;
        
        if (finishApplication)
            Environment.Exit(0);
    }
}
