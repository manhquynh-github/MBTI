using System;
using System.Globalization;
using System.Linq;

using MBTI.ConsoleApp.Screens;
using MBTI.Resources.Properties;

namespace MBTI.ConsoleApp.Commands
{
  public class ChangeLanguageCommand : ICommand
  {
    public ChangeLanguageCommand(CultureInfo cultureInfo)
    {
      CultureInfo = cultureInfo ??
        throw new ArgumentNullException(nameof(cultureInfo));

      Description = $"({cultureInfo.ThreeLetterISOLanguageName}) {cultureInfo.NativeName}";
      Action = UpdateLanguage;
    }

    public Func<ScreenBase> Action { get; }
    public CultureInfo CultureInfo { get; }
    public string Description { get; }

    private ScreenBase UpdateLanguage()
    {
      if (!Program.Current.SupportedLanguages.Contains(CultureInfo))
      {
        throw new NotSupportedException("Language not supported.");
      }

      CultureInfo.CurrentUICulture = CultureInfo;
      Content.Culture = CultureInfo;
      return new WelcomeScreen();
    }
  }
}