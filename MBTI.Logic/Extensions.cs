using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using MBTI.Models;

namespace MBTI.Logic
{
  public static class Extensions
  {
    public static string AlignCenter(this string text, int characterLimit)
    {
      if (text is null)
      {
        throw new ArgumentNullException(nameof(text));
      }

      if (characterLimit <= 0)
      {
        throw new ArgumentException("Value must be larger than 0.", nameof(characterLimit));
      }

      if (text == string.Empty)
      {
        return text;
      }

      IEnumerable<string> lines = text
        .WrapToLines(characterLimit)
        .Select(line => $"{new string(' ', characterLimit / 2 + 1 - (line.Length / 2))}{line}{new string(' ', characterLimit - (characterLimit / 2) + 1 - (line.Length - (line.Length / 2)))}");

      return string.Join(
        Environment.NewLine,
        lines);
    }

    public static string AlignLeft(this string text, int characterLimit, int padLeft = 0)
    {
      if (text is null)
      {
        throw new ArgumentNullException(nameof(text));
      }

      if (characterLimit <= 0)
      {
        throw new ArgumentException("Value must be larger than 0.", nameof(characterLimit));
      }

      if (characterLimit - padLeft <= 0)
      {
        throw new ArgumentException("Unable to pad within character limit.", nameof(padLeft));
      }

      if (text == string.Empty)
      {
        return text;
      }

      IEnumerable<string> lines = padLeft == 0
        ? text
        .WrapToLines(characterLimit)
        .Select(line => $"{line}{new string(' ', characterLimit - line.Length)}")
        : text
        .WrapToLines(characterLimit - padLeft)
        .Select(line => $"{new string(' ', padLeft)}{line}{new string(' ', characterLimit - line.Length)}");

      return string.Join(
        Environment.NewLine,
        lines);
    }

    public static string CapitalizeFirstLetter(this string text)
    {
      if (text is null)
      {
        throw new System.ArgumentNullException(nameof(text));
      }

      text = text.Trim();
      if (text == string.Empty)
      {
        return text;
      }

      return text[0].ToString().ToUpper() + text[1..];
    }

    public static string GetAcronym(this PersonalityPrefixes1 type)
    {
      return type.ToString().Substring(0, 1);
    }

    public static string GetAcronym(this PersonalityPrefixes2 type)
    {
      return type == PersonalityPrefixes2.Sensing
        ? type.ToString().Substring(0, 1)
        : "N";
    }

    public static string GetAcronym(this PersonalityPrefixes3 type)
    {
      return type.ToString().Substring(0, 1);
    }

    public static string GetAcronym(this PersonalityPrefixes4 type)
    {
      return type.ToString().Substring(0, 1);
    }

    public static string GetAcronym(this PersonalityType type)
    {
      return type.Prefix1.GetAcronym()
        + type.Prefix2.GetAcronym()
        + type.Prefix3.GetAcronym()
        + type.Prefix4.GetAcronym();
    }

    public static string Wrap(this string text, int characterLimit)
    {
      if (text is null)
      {
        throw new ArgumentNullException(nameof(text));
      }

      if (characterLimit <= 0)
      {
        throw new ArgumentException("Value must be larger than 0.", nameof(characterLimit));
      }

      if (text == string.Empty)
      {
        return text;
      }

      return string.Join(
        Environment.NewLine,
        WrapToLines(text, characterLimit));
    }

    public static IEnumerable<string> WrapToLines(this string text, int characterLimit)
    {
      if (text is null)
      {
        throw new ArgumentNullException(nameof(text));
      }

      if (characterLimit <= 0)
      {
        throw new ArgumentException("Value must be larger than 0.", nameof(characterLimit));
      }

      if (text == string.Empty)
      {
        return new string[] { text };
      }

      var lines = text.Split(Environment.NewLine);

      if (lines.Length > 1)
      {
        return lines.SelectMany(line => line.WrapToLines(characterLimit));
      }

      var sb = new StringBuilder();
      var words = text.Split(' ');
      var characterCount = 0;
      var result = new Queue<string>();
      foreach (var word in words)
      {
        if (characterCount > 0)
        {
          if (characterCount < characterLimit)
          {
            sb.Append(' ');
            characterCount += 1;
          }
          else
          {
            result.Enqueue(sb.ToString());
            sb.Clear();
            characterCount = 0;
          }
        }

        if (characterCount + word.Length > characterLimit)
        {
          result.Enqueue(sb.ToString());
          sb.Clear();
          characterCount = 0;
        }

        if (word.Length > characterLimit)
        {
          var temp = word;

          while (temp.Length > characterLimit)
          {
            sb.Append(temp[..characterLimit]);
            result.Enqueue(sb.ToString());
            sb.Clear();

            characterCount = 0;
            temp = temp[characterLimit..];
          }

          sb.Append(temp);
          characterCount += temp.Length;
        }
        else
        {
          sb.Append(word);
          characterCount += word.Length;
        }
      }

      result.Enqueue(sb.ToString());
      return result;
    }
  }
}