using System;
using System.Collections.Generic;
using System.Text;

using MBTI.ConsoleApp.Commands;
using MBTI.Resources.Properties;

namespace MBTI.ConsoleApp.Screens
{
  public class InstructionScreen : ScreenBase
  {
    private readonly IReadOnlyList<ICommand> _commands;

    public InstructionScreen(string instruction, Func<ScreenBase> nextScreen)
    {
      Instruction = instruction;
      _commands = new List<ICommand>()
      {
        new Command(
          "Continue",
          nextScreen),
      };
    }

    public override IReadOnlyList<ICommand> Commands => _commands;
    public string Instruction { get; }

    protected override void WriteDescriptionToConsole()
    {
      WriteDecoratedMessage(Content.STitle);
      WriteBeautifulMessage(Instruction);
      Console.WriteLine();
    }
  }
}