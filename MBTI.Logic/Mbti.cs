using System;
using System.Collections.Generic;

using MBTI.Models;

namespace MBTI.Logic
{
  public class Mbti
  {
    public Mbti(IReadOnlyList<Question> questions)
    {
      if (questions is null)
      {
        throw new ArgumentNullException(nameof(questions));
      }

      if (questions.Count != RequiredQuestionCount)
      {
        throw new InvalidOperationException(
          $"Number of question must be exactly {RequiredQuestionCount}.");
      }

      Calculate(questions);
    }

    public static int RequiredQuestionCount { get; } = 80;

    public int Extrovert { get; private set; }
    public int Feeling { get; private set; }
    public int Introvert { get; private set; }
    public int Intuition { get; private set; }
    public bool IsExtrovert => Extrovert > Introvert;
    public bool IsFeeling => !IsThinking;
    public bool IsIntrovert => !IsExtrovert;
    public bool IsIntuition => !IsSensing;
    public bool IsJudging => Judging > Perceptive;
    public bool IsPerceptive => !IsJudging;
    public bool IsSensing => Sensing > Intuition;
    public bool IsThinking => Thinking > Feeling;
    public int Judging { get; private set; }
    public int Perceptive { get; private set; }
    public int Sensing { get; private set; }
    public int Thinking { get; private set; }

    public PersonalityType GetFinalType()
    {
      return new PersonalityType()
      {
        Prefix1 = IsExtrovert
        ? PersonalityPrefixes1.Extrovert
        : PersonalityPrefixes1.Introvert,
        Prefix2 = IsSensing
        ? PersonalityPrefixes2.Sensing
        : PersonalityPrefixes2.Intuition,
        Prefix3 = IsThinking
        ? PersonalityPrefixes3.Thinking
        : PersonalityPrefixes3.Feeling,
        Prefix4 = IsJudging
        ? PersonalityPrefixes4.Judging
        : PersonalityPrefixes4.Perceptive,
      };
    }

    private void Calculate(IReadOnlyList<Question> questions)
    {
      if (questions is null)
      {
        throw new ArgumentNullException(nameof(questions));
      }

      for (var i = 0; i < questions.Count; i++)
      {
        Question question = questions[i];
        if (question is null)
        {
          throw new InvalidOperationException($"{nameof(question)} is null.");
        }

        var selectedIndex = question.SelectedIndex;
        if (selectedIndex < 1 || selectedIndex > 2)
        {
          throw new InvalidOperationException("Unexpected selected index.");
        }

        var column = i % 8;
        switch ((column % 2 == 1 ? column : column - 1) + selectedIndex)
        {
          case 2:
            Extrovert += 1; break;
          case 3:
            Introvert += 1; break;
          case 4:
            Sensing += 1; break;
          case 5:
            Intuition += 1; break;
          case 6:
            Thinking += 1; break;
          case 7:
            Feeling += 1; break;
          case 8:
          case 0:
            Judging += 1; break;
          case 9:
          case 1:
            Perceptive += 1; break;

          default:
            throw new InvalidOperationException("Unexpected value.");
        }
      }
    }
  }
}