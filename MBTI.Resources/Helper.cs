using System;
using System.Collections.Generic;
using System.Text.Json;

using MBTI.Models;
using MBTI.Resources.Properties;

namespace MBTI.Resources
{
  public static class Helper
  {
    public static Dictionary<string, PersonalityTypeDescription> GetDescriptions()
    {
      var resource = Content.Descriptions;

      return JsonSerializer.Deserialize<Dictionary<string, PersonalityTypeDescription>>(
        new ReadOnlySpan<byte>(resource));
    }

    public static List<Question> GetQuestions()
    {
      var resource = Content.Questions;

      return JsonSerializer.Deserialize<List<Question>>(
        new ReadOnlySpan<byte>(resource));
    }
  }
}