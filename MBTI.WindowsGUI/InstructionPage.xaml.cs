using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MBTI.WindowsGUI
{
  /// <summary>
  /// Interaction logic for InstructionPage.xaml
  /// </summary>
  public partial class InstructionPage : Page
  {
    private readonly Queue<string> _messages;
    private readonly DispatcherTimer _timer;
    private Func<Page> _getNextPageCallback;

    public InstructionPage()
    {
      InitializeComponent();
      _messages = new Queue<string>();
      _timer = new DispatcherTimer()
      {
        Interval = TimeSpan.FromSeconds(3),
      };
      _timer.Tick += _timer_Tick;
    }

    public InstructionPage DisplayMessages(params string[] messages)
    {
      if (messages is null
        || messages.Length == 0)
      {
        return this;
      }

      foreach (var message in messages)
      {
        if (string.IsNullOrEmpty(message))
        {
          continue;
        }

        _messages.Enqueue(message);
      }

      if (string.IsNullOrEmpty(TBlMessage.Text)
        && _messages.Count > 0)
      {
        TBlMessage.Text = _messages.Dequeue();
      }

      return this;
    }

    public InstructionPage ThenShowPage(Func<Page> getNextPageCallback)
    {
      if (getNextPageCallback is null)
      {
        throw new ArgumentNullException(nameof(getNextPageCallback));
      }

      if (!(_getNextPageCallback is null))
      {
        throw new InvalidOperationException("This action has already been set.");
      }

      _getNextPageCallback = getNextPageCallback;
      return this;
    }

    private async void _timer_Tick(object sender, EventArgs e)
    {
      if (_messages.Count > 0)
      {
        App.Current.FadeOutStoryboard.Begin(TBlMessage);
        await Task.Delay(TimeSpan.FromMilliseconds(400));
        TBlMessage.Text = _messages.Dequeue();
        App.Current.FadeInStoryboard.Begin(TBlMessage);
      }
      else
      {
        _timer.Stop();
        await App.Current.MainWindow.Navigate(_getNextPageCallback);
      }
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
      _timer.Start();
    }
  }
}