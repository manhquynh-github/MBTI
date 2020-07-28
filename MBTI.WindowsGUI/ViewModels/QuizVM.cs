using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using MBTI.Models;
using MBTI.Resources;

namespace MBTI.WindowsGUI.ViewModels
{
  public class QuizVM : VMBase
  {
    private int _currentIndex;

    public QuizVM()
    {
      List<Question> quiz = Helper.GetQuestions();
      Quiz = quiz.AsReadOnly();
    }

    public int CurrentIndex
    {
      get => _currentIndex;
      set
      {
        if (value < 0 || value > Quiz.Count)
        {
          throw new IndexOutOfRangeException(nameof(value));
        }

        SetProperty(ref _currentIndex, value);
        NotifyPropertyChanged(nameof(DisplayCurrentIndex));
        NotifyPropertyChanged(nameof(CurrentQuestion));
        NotifyPropertyChanged(nameof(DisplayQuestionContent));
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
  }
}