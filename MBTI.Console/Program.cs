using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using MBTI.ConsoleApp.Screens;

namespace MBTI.ConsoleApp
{
  public class Program
  {
    private static Program _current;

    public Program()
    {
      SupportedLanguages = new List<CultureInfo>()
      {
        CultureInfo.GetCultureInfo("en"),
        CultureInfo.GetCultureInfo("vi"),
      };

      Console.OutputEncoding = Encoding.UTF8;
    }

    public static Program Current => _current ??= new Program();

    public IEnumerable<CultureInfo> SupportedLanguages { get; }

    public static void Main(string[] args)
    {
      Current.Run();
    }

    public void Run()
    {
      new WelcomeScreen().WriteToConsole();
    }
  }
}