using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace MBTI
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    public async Task Navigate(Page page)
    {
      App.Current.FadeOutStoryboard.Begin(MainFrame);
      await Task.Delay(400);
      MainFrame.Navigate(page);
    }

    private void MainFrame_Navigated(object sender, NavigationEventArgs e)
    {
      App.Current.FadeInStoryboard.Begin(MainFrame);
      if (MainFrame.CanGoBack)
      {
        MainFrame.RemoveBackEntry();
      }
    }
  }
}