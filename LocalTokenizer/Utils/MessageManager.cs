using LocalTokenizer.Constants.Messages;
using System;
using System.Collections.Generic;

namespace LocalTokenizer.Utils;

public static class MessageManager
{

    private static void SendMessage(string message, bool finishApplication, string messageType)
    {
        var originalColor = Console.ForegroundColor;

        if(messageType == MessagesConstants.SuccessMessage)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(message);
        }
        
        Console.ForegroundColor = originalColor;
        if (finishApplication)
            Environment.Exit(0);
    }
    public static void SendErrorMessage(string message, bool finishApplication)
    {
        SendMessage(message, finishApplication, MessagesConstants.ErrorMessage);
    }

    public static void SendSuccessMessage(string message, bool finishApplication)
    {
        SendMessage(message, finishApplication, MessagesConstants.SuccessMessage);
    }

    public static void SendCustomMessage(List<CustomMessageFragment> messageFragments, bool v)
    {
        foreach(var message in messageFragments)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = message.Color;            

            if (message.isToBreakLine)
            {
                Console.WriteLine(message.TextMessage);
            }else
            {
                Console.Write(message.TextMessage);
            }

            Console.ForegroundColor = originalColor;
        }
    }
}
