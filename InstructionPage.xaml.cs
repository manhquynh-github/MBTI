using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MBTI
{
  /// <summary>
  /// Interaction logic for InstructionPage.xaml
  /// </summary>
  public partial class InstructionPage : Page
  {
    private readonly Storyboard _fadeInStoryboard;
    private readonly Storyboard _fadeOutStoryboard;
    private readonly Queue<string> _messages;
    private readonly Page _nextPage;
    private readonly DispatcherTimer _timer;

    public InstructionPage(Page nextPage, params string[] messages)
    {
      if (nextPage is null)
      {
        throw new ArgumentNullException(nameof(nextPage));
      }

      if (messages is null)
      {
        throw new ArgumentNullException(nameof(messages));
      }

      InitializeComponent();
      _nextPage = nextPage;
      _messages = new Queue<string>(messages);
      _timer = new DispatcherTimer()
      {
        Interval = TimeSpan.FromSeconds(3),
      };
      _timer.Tick += _timer_Tick;
      _fadeInStoryboard = (Storyboard)Application.Current.Resources["FadeInStoryboard"];
      _fadeOutStoryboard = (Storyboard)Application.Current.Resources["FadeOutStoryboard"];

      if (_messages.Count > 0)
      {
        TBlMessage.Text = _messages.Dequeue();
      }
    }

    private async void _timer_Tick(object sender, EventArgs e)
    {
      if (_messages.Count > 0)
      {
        _fadeOutStoryboard.Begin(TBlMessage);
        await Task.Delay(TimeSpan.FromMilliseconds(400));
        TBlMessage.Text = _messages.Dequeue();
        _fadeInStoryboard.Begin(TBlMessage);
      }
      else
      {
        _timer.Stop();
        await App.Current.MainWindow.Navigate(_nextPage);
      }
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
      _timer.Start();
    }
  }
}