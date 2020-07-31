using System;
using System.Collections.Generic;

using MBTI.ConsoleApp.Commands;
using MBTI.Logic;
using MBTI.Resources.Properties;

namespace MBTI.ConsoleApp.Screens
{
  public class AboutScreen : ScreenBase
  {
    private readonly IReadOnlyList<ICommand> _commands;

    public AboutScreen()
    {
      _commands = new List<ICommand>()
      {
        new Command(
        "Read more at Wikipedia in a browser",
        () =>
        {
          HelperClass.OpenBrowser("https://en.wikipedia.org/wiki/Myers%E2%80%93Briggs_Type_Indicator");
          return this;
        }),
        new BackCommand(new WelcomeScreen()),
      };
    }

    public override IReadOnlyList<ICommand> Commands => _commands;

    protected override void WriteDescriptionToConsole()
    {
      WriteDecoratedMessage(Content.STitle);
      WriteBeautifulMessage(Content.SMBTIDescription + " (Wikipedia)");
      Console.WriteLine();
    }
  }
}