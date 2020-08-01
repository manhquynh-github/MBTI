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

    public StudyScreen(ResultScreen resultScreen, PersonalityType type)
    {
      Type = type;
      ResultScreen = resultScreen;
      _descriptions = Resources.HelperClass.GetDescriptions();

      var commands = new List<ICommand>()
      {
        new Command("Choose another personality type", () => new ChoosePersonalityTypeScreen(this)),
        new Command(Content.SHomeTooltip, () => new WelcomeScreen()),
      };

      if (!(resultScreen is null))
      {
        commands.Insert(
          commands.Count - 2,
          new BackCommand("Back to results", ResultScreen));
      }

      _commands = commands;
    }

    public override IReadOnlyList<ICommand> Commands => _commands;
    public IEnumerable<string> PersonalityTypeAcronyms => _descriptions.Keys;
    public ResultScreen ResultScreen { get; }
    public PersonalityType Type { get; }

    protected override void WriteDescriptionToConsole()
    {
      WriteDecoratedMessage(Content.STitle);
      var acronym = Type.GetAcronym();
      WriteBeautifulSeparator();
      WriteBeautifulMessage(acronym);
      WriteBeautifulSeparator();
      WriteAcronymDetails();
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

    private string EmphasizeCharacter(string text, int characterIndex)
    {
      return $"{text[..characterIndex]}[{text[characterIndex]}]{text[(characterIndex + 1)..]}";
    }

    private void WriteAcronymDetails()
    {
      WriteBeautifulMessage(
        $"{EmphasizeCharacter(Type.Prefix1.ToString().ToUpper(), 0)} - " +
        $"{(Type.Prefix1 == PersonalityPrefixes1.Extrovert ? Content.SExtrovert : Content.SIntrovert).ToUpper()}");

      WriteBeautifulMessage(
        $"{(EmphasizeCharacter(Type.Prefix2.ToString().ToUpper(), Type.Prefix2 == PersonalityPrefixes2.Intuition ? 1 : 0))} - " +
        $"{(Type.Prefix2 == PersonalityPrefixes2.Sensing ? Content.SSensing : Content.SIntuition).ToUpper()}");

      WriteBeautifulMessage(
        $"{EmphasizeCharacter(Type.Prefix3.ToString().ToUpper(), 0)} - " +
        $"{(Type.Prefix3 == PersonalityPrefixes3.Thinking ? Content.SThinking : Content.SFeeling).ToUpper()} ");

      WriteBeautifulMessage(
        $"{EmphasizeCharacter(Type.Prefix4.ToString().ToUpper(), 0)} - " +
        $"{(Type.Prefix4 == PersonalityPrefixes4.Judging ? Content.SJudging : Content.SPerceptive).ToUpper()}");
    }
  }
}