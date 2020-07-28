using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;

using MBTI.Models;
using MBTI.Resources.Properties;

namespace MBTI.Resources
{
  public static class Content
  {
    public static Dictionary<string, PersonalityTypeDescription> GetDescriptions(CultureInfo cultureInfo)
    {
      Properties.Content.Culture = cultureInfo;
      var resource = Properties.Content.Descriptions;

      return JsonSerializer.Deserialize<Dictionary<string, PersonalityTypeDescription>>(
        new ReadOnlySpan<byte>(resource));
    }

    public static List<Question> GetQuestions(CultureInfo cultureInfo)
    {
      Properties.Content.Culture = cultureInfo;
      var resource = Properties.Content.Questions;

      return JsonSerializer.Deserialize<List<Question>>(
        new ReadOnlySpan<byte>(resource));
    }
  }
}