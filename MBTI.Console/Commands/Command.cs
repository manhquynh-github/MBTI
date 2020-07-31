using System;

using MBTI.ConsoleApp.Screens;

namespace MBTI.ConsoleApp.Commands
{
  public class Command : ICommand
  {
    public Command(string description, Func<ScreenBase> action)
    {
      if (string.IsNullOrWhiteSpace(description))
      {
        throw new ArgumentException("Value must not be null or white space.", nameof(description));
      }

      Description = description;
      Action = action
        ?? throw new ArgumentNullException(nameof(action));
    }

    public Func<ScreenBase> Action { get; }
    public string Description { get; }
  }
}