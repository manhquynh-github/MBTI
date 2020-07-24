using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;

namespace MBTI.WindowsGUI
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();

      SupportedLanguages = new List<CultureInfo>()
      {
        CultureInfo.GetCultureInfo("en"),
        CultureInfo.GetCultureInfo("vi"),
      };

      LanguageSources = new List<Func<CultureInfo, string>>()
      {
         info => $"/ResourceDictionaries/UIContent/UIContent.{info.TwoLetterISOLanguageName}.xaml",
         info => $"/ResourceDictionaries/Content/Questions.{info.TwoLetterISOLanguageName}.xaml",
         info => $"/ResourceDictionaries/Content/Descriptions.{info.TwoLetterISOLanguageName}.xaml",
      };

      FadeInStoryboard = (Storyboard)Resources["FadeInStoryboard"];
      FadeOutStoryboard = (Storyboard)Resources["FadeOutStoryboard"];
    }

    public Storyboard FadeInStoryboard { get; }
    public Storyboard FadeOutStoryboard { get; }
    public IEnumerable<CultureInfo> SupportedLanguages { get; }
    public static new App Current => (App)Application.Current;
    public new MainWindow MainWindow => (MainWindow)Application.Current.MainWindow;
    protected IList<Func<CultureInfo, string>> LanguageSources { get; }

    public void UpdateLanguage(CultureInfo cultureInfo)
    {
      if (cultureInfo is null)
      {
        throw new ArgumentNullException(nameof(cultureInfo));
      }

      if (!SupportedLanguages.Contains(cultureInfo))
      {
        throw new NotSupportedException();
      }

      Resources.MergedDictionaries.Clear();

      foreach (ResourceDictionary resource in GetLanguageResources(cultureInfo))
      {
        Resources.MergedDictionaries.Add(resource);
      }

      CultureInfo.CurrentCulture = cultureInfo;
      CultureInfo.CurrentUICulture = cultureInfo;
    }

    protected IEnumerable<ResourceDictionary> GetLanguageResources(CultureInfo cultureInfo)
    {
      for (int i = 0; i < LanguageSources.Count; i++)
      {
        string source = LanguageSources[i](cultureInfo);

        ResourceDictionary dict;

        try
        {
          dict = new ResourceDictionary()
          {
            Source = new Uri(source, UriKind.Relative),
          };
        }
        catch (IOException)
        {
          source = LanguageSources[i](new CultureInfo("vi"));

          try
          {
            dict = new ResourceDictionary()
            {
              Source = new Uri(source, UriKind.Relative),
            };
          }
          catch (IOException)
          {
            throw new InvalidOperationException("Default language source not found");
          }
        }

        yield return dict;
      }
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
      base.MainWindow.Close();
    }

    private void MaximizeButton_Click(object sender, RoutedEventArgs e)
    {
      if (MainWindow.WindowState == WindowState.Normal)
      {
        base.MainWindow.WindowState = WindowState.Maximized;
      }
      else
      {
        base.MainWindow.WindowState = WindowState.Normal;
      }
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
      base.MainWindow.WindowState = WindowState.Minimized;
    }
  }
}