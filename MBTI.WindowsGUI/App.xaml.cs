using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

using MBTI.Resources.Properties;
using MBTI.WindowsGUI.ViewModels;

namespace MBTI.WindowsGUI
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private CultureInfo _language;

    public App()
    {
      InitializeComponent();

      SupportedLanguages = new List<CultureInfo>()
      {
        CultureInfo.GetCultureInfo("en"),
        CultureInfo.GetCultureInfo("vi"),
      };

      Language = SupportedLanguages.First();

      FadeInStoryboard = (Storyboard)Resources["FadeInStoryboard"];
      FadeOutStoryboard = (Storyboard)Resources["FadeOutStoryboard"];

      if (FadeInStoryboard == null
       || FadeOutStoryboard == null)
      {
        throw new InvalidOperationException(
          "Unable to get resource dictionary for fade animations.");
      }
    }

    public Storyboard FadeInStoryboard { get; }

    public Storyboard FadeOutStoryboard { get; }

    public CultureInfo Language
    {
      get => _language;
      set
      {
        _language = value;
        UpdateLanguage(value);
      }
    }

    public IEnumerable<CultureInfo> SupportedLanguages { get; }
    public static new App Current => (App)Application.Current;
    public new MainWindow MainWindow => (MainWindow)Application.Current.MainWindow;

    private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
      ShowExceptionDialog(e.Exception);
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
      base.MainWindow.Close();
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      ShowExceptionDialog((Exception)e.ExceptionObject);
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

    private void ShowExceptionDialog(Exception e)
    {
      var message = $"Unexpected error has occured." +
        $"{Environment.NewLine}" +
        $"{Environment.NewLine}" +
        $"Additional information: {e.Message}";

      MessageBox.Show(
        message,
        "Error",
        MessageBoxButton.OK,
        MessageBoxImage.Error,
        MessageBoxResult.OK);
    }

    private void UpdateLanguage(CultureInfo cultureInfo)
    {
      if (cultureInfo is null)
      {
        throw new ArgumentNullException(nameof(cultureInfo));
      }

      if (!SupportedLanguages.Contains(cultureInfo))
      {
        throw new NotSupportedException();
      }

      CultureInfo.CurrentUICulture = cultureInfo;
      ResourcesVM.Instance.CurrentCulture = cultureInfo;
    }
  }
}