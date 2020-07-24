using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace MBTI.WindowsGUI
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

    public async Task Navigate(Func<Page> getPageCallBack)
    {
      if (getPageCallBack is null)
      {
        throw new ArgumentNullException(nameof(getPageCallBack));
      }

      Page page = await Dispatcher.InvokeAsync(getPageCallBack);
      await Navigate(page);
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