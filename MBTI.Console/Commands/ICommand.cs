using System;

using MBTI.ConsoleApp.Screens;

namespace MBTI.ConsoleApp.Commands
{
  public interface ICommand
  {
    Func<ScreenBase> Action { get; }
    string Description { get; }
  }
}