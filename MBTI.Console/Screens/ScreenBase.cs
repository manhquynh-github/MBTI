using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MBTI.ConsoleApp.Commands;
using MBTI.Logic;

namespace MBTI.ConsoleApp.Screens
{
  public abstract class ScreenBase
  {
    protected static readonly int MaxDecorableMessageLength = 76;
    protected static readonly int PaddingLeft = 3;
    public abstract IReadOnlyList<ICommand> Commands { get; }

    public void WriteToConsole()
    {
      var invalidSelection = false;
      ScreenBase nextScreen = this;

      do
      {
        Console.Clear();
        WriteDescriptionToConsole();
        WriteCommands();
        Console.WriteLine();

        if (invalidSelection)
        {
          Console.WriteLine($"{new string(' ', PaddingLeft)}Please try again.");
        }

        var selection = ReadSelection();
        if (selection < 1)
        {
          invalidSelection = true;
          continue;
        }

        invalidSelection = false;
        nextScreen = ExcecuteCommand(selection - 1);
      } while (nextScreen == this);

      if (!(nextScreen is null))
      {
        nextScreen.WriteToConsole();
      }
    }

    protected string BeautifyMessage(string message)
    {
      return message.AlignLeft(MaxDecorableMessageLength + 4 - PaddingLeft, PaddingLeft);
    }

    protected string DecorateMessage(string message)
    {
      if (string.IsNullOrWhiteSpace(message))
      {
        throw new ArgumentException("Value must not be null or white space.", nameof(message));
      }

      var lines = message
        .AlignCenter(MaxDecorableMessageLength)
        .Split(
          Environment.NewLine,
          StringSplitOptions.RemoveEmptyEntries);

      var sb = new StringBuilder();

      sb.AppendLine(new string('*', MaxDecorableMessageLength + 4))
        .AppendLine($"*{new string(' ', 1 + MaxDecorableMessageLength + 1)}*");
      foreach (var line in lines)
      {
        sb.Append($"*{line}").AppendLine("*");
      }
      sb.AppendLine($"*{new string(' ', 1 + MaxDecorableMessageLength + 1)}*")
        .AppendLine(new string('*', MaxDecorableMessageLength + 4));

      return sb.ToString();
    }

    protected void WriteBeautifulMessage(string message)
    {
      Console.WriteLine(BeautifyMessage(message));
    }

    protected void WriteBeautifulSeparator()
    {
      WriteBeautifulMessage(new string('-', MaxDecorableMessageLength - PaddingLeft));
    }

    protected void WriteDecoratedMessage(string message)
    {
      Console.WriteLine(DecorateMessage(message));
    }

    protected abstract void WriteDescriptionToConsole();

    private ScreenBase ExcecuteCommand(int index)
    {
      if (index < 0 || index >= Commands.Count)
      {
        throw new IndexOutOfRangeException(nameof(index));
      }

      ICommand command = Commands[index];
      return command.Action();
    }

    private int ReadSelection()
    {
      Console.Write($"{new string(' ', PaddingLeft)}Your input: ");
      var input = Console.ReadLine();
      int selection;
      try
      {
        selection = int.Parse(input);
      }
      catch (Exception e) when (
      e is ArgumentNullException
      || e is FormatException
      || e is OverflowException)
      {
        return 0;
      }

      if (selection < 1 || selection > Commands.Count)
      {
        return 0;
      }

      return selection;
    }

    private void WriteCommands()
    {
      if (Commands == null)
      {
        throw new InvalidOperationException($"{nameof(Commands)} is null.");
      }

      if (Commands.Count == 0)
      {
        throw new InvalidOperationException($"{nameof(Commands)} must not be empty.");
      }

      for (var i = 0; i < Commands.Count; i++)
      {
        ICommand command = Commands[i];
        Console.WriteLine($"{i + 1}. {command.Description}".AlignLeft(MaxDecorableMessageLength - PaddingLeft, PaddingLeft));
      }
    }
  }
}