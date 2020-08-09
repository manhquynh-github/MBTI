using System;
using System.Collections.Generic;
using System.Reflection;

using MBTI.ConsoleApp.Commands;
using MBTI.Logic;
using MBTI.Resources.Properties;

namespace MBTI.ConsoleApp.Screens
{
  public class AboutScreen : ScreenBase
  {
    private readonly IReadOnlyList<ICommand> _commands;
    private readonly string _versionString;

    public AboutScreen()
    {
      var versionString = Assembly
        .GetExecutingAssembly()
        .GetName().Version
        .ToString();
      _versionString = $"v{versionString}";

      _commands = new List<ICommand>()
      {
        new Command(
          Content.SReadMoreAtWikipedia,
          () =>
          {
            HelperClass.OpenBrowser("https://en.wikipedia.org/wiki/Myers%E2%80%93Briggs_Type_Indicator");
            return this;
          }),
        new Command(
          Content.SGoToGitHubSource,
          () =>
          {
            HelperClass.OpenBrowser("https://github.com/manhquynh-github/MBTI");
            return this;
          }),
        new BackCommand(new WelcomeScreen()),
      };
    }

    public override IReadOnlyList<ICommand> Commands => _commands;

    protected override void WriteDescriptionToConsole()
    {
      WriteDecoratedMessage(Content.STitle);
      WriteBeautifulMessage($"{Content.SMBTIDescription} (Wikipedia)");
      Console.WriteLine();
      WriteBeautifulMessage($"MBTI {_versionString}");
      WriteBeautifulMessage($"Project can be found at GitHub page");
      Console.WriteLine();
    }
  }
}