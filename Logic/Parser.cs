using System;
using System.Collections.Generic;
using System.Linq;

using MBTI.Models;

namespace MBTI.Logic
{
  public static class Parser
  {
    public static void Parse(string[] source, out PersonalityTypeDescription value)
    {
      if (source.Length != 2)
      {
        throw new ArgumentException(nameof(source));
      }

      value = new PersonalityTypeDescription()
      {
        Description = string.Join(
          Environment.NewLine,
          source[0].Trim()
                   .Split("\n", StringSplitOptions.RemoveEmptyEntries)
                   .Select(v => $"- {v.Trim()}.")),
        SuggestedJobs = string.Join(
          Environment.NewLine,
          source[1].Trim()
                   .Split("\n", StringSplitOptions.RemoveEmptyEntries)
                   .Select(v => $"- {v.Trim()}.")),
      };
    }

    public static void Parse(string[] source, out IList<Question> value)
    {
      if (source.Length % 3 != 0)
      {
        throw new ArgumentException(nameof(source));
      }

      value = new List<Question>();

      for (int i = 0; i < source.Length / 3; i++)
      {
        value.Add(new Question()
        {
          Content = source[i * 3].Trim(),
          FirstChoice = new Choice()
          {
            Content = source[i * 3 + 1].Trim(),
          },
          SecondChoice = new Choice()
          {
            Content = source[i * 3 + 2].Trim(),
          },
        });
      }
    }
  }
}