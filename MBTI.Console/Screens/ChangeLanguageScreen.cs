using System;
using System.Collections.Generic;
using System.Linq;

using MBTI.ConsoleApp.Commands;
using MBTI.Resources.Properties;

namespace MBTI.ConsoleApp.Screens
{
  public class ChangeLanguageScreen : ScreenBase
  {
    private readonly IReadOnlyList<ICommand> _commands;

    public ChangeLanguageScreen()
    {
      var commands = new List<ICommand>();
      commands.AddRange(
        Program.Current.SupportedLanguages
        .Select(ci => new ChangeLanguageCommand(ci)));
      commands.Add(new BackCommand(new WelcomeScreen()));
      _commands = commands;
    }

    public override IReadOnlyList<ICommand> Commands => _commands;

    protected override void WriteDescriptionToConsole()
    {
      WriteDecoratedMessage(Content.STitle);
    }
  }
}