using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalTokenizer.Constants.Messages;

public class CustomMessageFragment
{
    public string TextMessage;
    public ConsoleColor Color;
    public bool isToBreakLine;

    public CustomMessageFragment(string textMessage, ConsoleColor color, bool isToBreakLine = true)
    {
        TextMessage = textMessage;
        Color = color;
        this.isToBreakLine = isToBreakLine;
    }

    public CustomMessageFragment(string textMessage, bool isToBreakLine = true)
    {
        TextMessage = textMessage;
        this.isToBreakLine = isToBreakLine;
        Color = Console.ForegroundColor;
    }
}
