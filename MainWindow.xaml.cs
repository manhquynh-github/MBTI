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
    private readonly Storyboard _fadeInStoryboard;
    private readonly Storyboard _fadeOutStoryboard;

    public MainWindow()
    {
      InitializeComponent();
      _fadeInStoryboard = (Storyboard)Application.Current.Resources["FadeInStoryboard"];
      _fadeOutStoryboard = (Storyboard)Application.Current.Resources["FadeOutStoryboard"];
    }

    public async Task Navigate(Page page)
    {
      _fadeOutStoryboard.Begin(MainFrame);
      await Task.Delay(400);
      MainFrame.Navigate(page);
    }

    private void MainFrame_Navigated(object sender, NavigationEventArgs e)
    {
      _fadeInStoryboard.Begin(MainFrame);
      if (MainFrame.CanGoBack)
      {
        MainFrame.RemoveBackEntry();
      }
    }
  }
}