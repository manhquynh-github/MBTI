using System;
using System.Collections.Generic;

using MBTI.ConsoleApp.Commands;
using MBTI.Logic;
using MBTI.Resources.Properties;

namespace MBTI.ConsoleApp.Screens
{
  public class WelcomeScreen : ScreenBase
  {
    private readonly IReadOnlyList<ICommand> _commands;

    public WelcomeScreen()
    {
      _commands = new List<ICommand>()
      {
        new Command(
          Content.SQuiz.CapitalizeFirstLetter(),
          () => new InstructionScreen(
            $"{Content.SInstruction1}{Environment.NewLine}{Content.SInstruction2}",
            () => new QuizScreen())),
        new Command(
          Content.SStudy.CapitalizeFirstLetter(),
          () => new StudyScreen()),
        new Command(
          Content.SChangeLanguage,
          () => new ChangeLanguageScreen()),
        new Command(
          Content.SAbout,
          () => new AboutScreen()),
        new Command(
          Content.SExit,
          () => null),
      };
    }

    public override IReadOnlyList<ICommand> Commands => _commands;

    protected override void WriteDescriptionToConsole()
    {
      var title = $"{Content.STitle}{Environment.NewLine}{Content.SSubtitle}";
      WriteDecoratedMessage(title);
    }
  }
}