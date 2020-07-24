using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

using MBTI.Logic;

namespace MBTI.WindowsGUI
{
  /// <summary>
  /// Interaction logic for AboutPage.xaml
  /// </summary>
  public partial class AboutPage : Page
  {
    public AboutPage()
    {
      InitializeComponent();
      string versionString = Assembly
        .GetExecutingAssembly()
        .GetName().Version
        .ToString();
      TblVersion.Text = $"v{versionString}";
    }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
      await App.Current.MainWindow.Navigate(() => new WelcomePage());
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
      Hyperlink hyperlink = (Hyperlink)sender;
      if (!(hyperlink is null))
      {
        HelperClass.OpenBrowser(hyperlink.NavigateUri.ToString());
        e.Handled = true;
      }
    }
  }
}