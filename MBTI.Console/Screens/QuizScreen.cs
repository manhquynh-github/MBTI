using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

using MBTI.ConsoleApp.Commands;
using MBTI.Logic;
using MBTI.Models;
using MBTI.Resources.Properties;

namespace MBTI.ConsoleApp.Screens
{
  public class QuizScreen : ScreenBase
  {
    private IReadOnlyList<ICommand> _commands;
    private int _currentIndex;

    public QuizScreen()
    {
      List<Question> quiz = Resources.HelperClass.GetQuestions();
      Quiz = quiz.ToImmutableList();
    }

    public override IReadOnlyList<ICommand> Commands => _commands;

    public int CurrentIndex
    {
      get => _currentIndex;
      set
      {
        if (value < 0 || value > Quiz.Count)
        {
          throw new IndexOutOfRangeException(nameof(value));
        }

        if (_currentIndex != value)
        {
          _currentIndex = value;
        }
      }
    }

    public Question CurrentQuestion => Quiz[CurrentIndex];
    public int DisplayCurrentIndex => CurrentIndex + 1;

    public string DisplayQuestionContent => CurrentQuestion.Content[0]
      .ToString()
      .ToUpper() + CurrentQuestion.Content.Substring(1);

    public IReadOnlyList<Question> Quiz { get; }

    public bool MoveNext()
    {
      if (CurrentIndex >= Quiz.Count - 1)
      {
        return false;
      }

      CurrentIndex++;
      return true;
    }

    public bool MovePrevious()
    {
      if (CurrentIndex <= 0)
      {
        return false;
      }

      CurrentIndex--;
      return true;
    }

    protected override void WriteDescriptionToConsole()
    {
      WriteDecoratedMessage(Content.STitle);
      WriteBeautifulMessage($"{Content.SQuestion} {CurrentIndex + 1}/{Quiz.Count}");
      WriteBeautifulSeparator();
      WriteBeautifulMessage(DisplayQuestionContent);
      WriteBeautifulMessage($"(1) {CurrentQuestion.FirstChoice}");
      WriteBeautifulMessage($"(2) {CurrentQuestion.SecondChoice}");
      WriteBeautifulSeparator();

      _commands = new List<ICommand>()
      {
        new Command("Select (1)", SelectFirst),
        new Command("Select (2)", SelectSecond),
        new Command("Back", MoveBack),
        new Command(Content.SHomeTooltip, () => new WelcomeScreen()),
      };
    }

    private ScreenBase GetResultScreen()
    {
      return new InstructionScreen(
        $"{Content.SInstruction3}",
        () => new ResultScreen(new Mbti(Quiz)));
    }

    private ScreenBase MoveBack()
    {
      if (!MovePrevious())
      {
        return new WelcomeScreen();
      }

      return this;
    }

    private ScreenBase MoveForward()
    {
      if (MoveNext())
      {
        return this;
      }
      else
      {
        return GetResultScreen();
      }
    }

    private ScreenBase SelectFirst()
    {
      CurrentQuestion.SelectedIndex = 1;
      return MoveForward();
    }

    private ScreenBase SelectSecond()
    {
      CurrentQuestion.SelectedIndex = 1;
      return MoveForward();
    }
  }
}