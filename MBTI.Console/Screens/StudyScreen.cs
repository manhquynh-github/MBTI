using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MBTI.ConsoleApp.Commands;
using MBTI.Logic;
using MBTI.Models;
using MBTI.Resources;
using MBTI.Resources.Properties;

namespace MBTI.ConsoleApp.Screens
{
  public class StudyScreen : ScreenBase
  {
    private readonly IReadOnlyList<ICommand> _commands;

    private readonly Dictionary<string, PersonalityTypeDescription> _descriptions;
    private readonly ResultScreen _resultScreen;

    public StudyScreen() : this(new PersonalityType()
    {
      Prefix1 = PersonalityPrefixes1.Extrovert,
      Prefix2 = PersonalityPrefixes2.Sensing,
      Prefix3 = PersonalityPrefixes3.Thinking,
      Prefix4 = PersonalityPrefixes4.Judging
    })
    {
    }

    public StudyScreen(PersonalityType type) : this(null, type)
    {
    }

    public StudyScreen(ResultScreen resultScreen) : this(
      resultScreen,
      resultScreen.Mbti.GetFinalType())
    {
    }

    private StudyScreen(ResultScreen resultScreen, PersonalityType type)
    {
      Type = type;
      _resultScreen = resultScreen;
      _descriptions = Resources.HelperClass.GetDescriptions();

      var commands = new List<ICommand>()
      {
        new Command("Input a personality type", InputPersonalityType),
        new Command(Content.SHomeTooltip, () => new WelcomeScreen()),
      };

      if (!(resultScreen is null))
      {
        commands.Insert(
          commands.Count - 2,
          new BackCommand("Back to results", _resultScreen));
      }

      _commands = commands;
    }

    public override IReadOnlyList<ICommand> Commands => _commands;

    public PersonalityType Type { get; }

    protected override void WriteDescriptionToConsole()
    {
      WriteDecoratedMessage(Content.STitle);
      var acronym = Type.GetAcronym();
      WriteBeautifulSeparator();
      WriteBeautifulMessage(acronym);
      WriteBeautifulSeparator();
      PersonalityTypeDescription description = _descriptions[acronym];
      WriteBeautifulMessage(Content.SPersonality);
      WriteBeautifulSeparator();
      WriteBeautifulMessage(string.Join(
        Environment.NewLine,
        description.Descriptions.Select(line => $"- {line}.")));
      WriteBeautifulSeparator();
      WriteBeautifulMessage(Content.SSuggestedJobs);
      WriteBeautifulSeparator();
      WriteBeautifulMessage(string.Join(
        Environment.NewLine,
        description.SuggestedJobs.Select(line => $"- {line}.")));
      WriteBeautifulSeparator();
      Console.WriteLine();
    }

    private ScreenBase InputPersonalityType()
    {
      Console.Write($"{new string(' ', PaddingLeft)}Input a personality type: ");
      var typeStr = Console.ReadLine();
      PersonalityType type;
      try
      {
        type = PersonalityType.Parse(typeStr);
      }
      catch (Exception e) when (
      e is ArgumentException
      || e is FormatException)
      {
        WriteBeautifulMessage("Please try again.");
        Console.ReadKey();
        return this;
      }

      return new StudyScreen(_resultScreen, type);
    }
  }
}