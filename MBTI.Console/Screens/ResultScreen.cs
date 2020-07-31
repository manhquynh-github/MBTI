using System;
using System.Collections.Generic;
using System.Text;

using MBTI.ConsoleApp.Commands;
using MBTI.Logic;
using MBTI.Resources.Properties;

namespace MBTI.ConsoleApp.Screens
{
  public class ResultScreen : ScreenBase
  {
    private readonly IReadOnlyList<ICommand> _commands;

    public ResultScreen(Mbti mbti)
    {
      Mbti = mbti;

      _commands = new List<ICommand>()
      {
        new Command(Content.SStudy.CapitalizeFirstLetter(), () => new StudyScreen(this)),
      };
    }

    public override IReadOnlyList<ICommand> Commands => _commands;
    public Mbti Mbti { get; }

    protected override void WriteDescriptionToConsole()
    {
      WriteDecoratedMessage(Content.STitle);
      WriteBeautifulMessage($"{Content.SInstruction4} {Mbti.GetFinalType().GetAcronym()}");
      WriteBeautifulSeparator();
      WriteBeautifulMessage($"{Content.SExtrovert}: {Mbti.Extrovert / 20.0:P}");
      WriteBeautifulMessage($"{Content.SIntrovert}: {Mbti.Introvert / 20.0:P}");
      WriteBeautifulSeparator();
      WriteBeautifulMessage($"{Content.SSensing}: {Mbti.Sensing / 20.0:P}");
      WriteBeautifulMessage($"{Content.SIntuition}: {Mbti.Intuition / 20.0:P}");
      WriteBeautifulSeparator();
      WriteBeautifulMessage($"{Content.SThinking}: {Mbti.Thinking / 20.0:P}");
      WriteBeautifulMessage($"{Content.SFeeling}: {Mbti.Feeling / 20.0:P}");
      WriteBeautifulSeparator();
      WriteBeautifulMessage($"{Content.SJudging}: {Mbti.Judging / 20.0:P}");
      WriteBeautifulMessage($"{Content.SPerceptive}: {Mbti.Perceptive / 20.0:P}");
      WriteBeautifulSeparator();
      Console.WriteLine();
    }
  }
}