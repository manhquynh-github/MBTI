using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
          source[0].Split('&', StringSplitOptions.RemoveEmptyEntries).Select(v => $"- {v}.")),
        SuggestedJobs = string.Join(
          Environment.NewLine,
          source[1].Split('&', StringSplitOptions.RemoveEmptyEntries).Select(v => $"- {v}.")),
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
          Content = source[i * 3],
          FirstChoice = new Choice()
          {
            Content = source[i * 3 + 1],
          },
          SecondChoice = new Choice()
          {
            Content = source[i * 3 + 2],
          },
        });
      }
    }
  }
}