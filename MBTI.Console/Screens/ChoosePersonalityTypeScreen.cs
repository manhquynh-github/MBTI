using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MBTI.ConsoleApp.Commands;
using MBTI.Models;
using MBTI.Resources.Properties;

namespace MBTI.ConsoleApp.Screens
{
  public class ChoosePersonalityTypeScreen : ScreenBase
  {
    private readonly IReadOnlyList<ICommand> _commands;

    public ChoosePersonalityTypeScreen(StudyScreen studyScreen)
    {
      var commands = studyScreen.PersonalityTypeAcronyms
        .Select(v => new Command(
          v,
          () => new StudyScreen(
            studyScreen.ResultScreen,
            PersonalityType.Parse(v))))
        .ToList();

      commands.Add(new BackCommand(studyScreen));
      _commands = commands;
    }

    public override IReadOnlyList<ICommand> Commands => _commands;

    protected override void WriteDescriptionToConsole()
    {
      WriteDecoratedMessage(Content.STitle);
    }
  }
}