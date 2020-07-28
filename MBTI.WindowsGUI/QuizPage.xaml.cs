using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using MBTI.Logic;
using MBTI.Models;
using MBTI.WindowsGUI.ViewModels;

namespace MBTI.WindowsGUI
{
  /// <summary>
  /// Interaction logic for QuizPage.xaml
  /// </summary>
  public partial class QuizPage : Page
  {
    private bool _isTransitioning;

    public QuizPage()
    {
      InitializeComponent();
      QuizVM = new QuizVM();
      DataContext = QuizVM;
    }

    public QuizVM QuizVM { get; }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
      if (QuizVM.CurrentIndex == 0)
      {
        await App.Current.MainWindow.Navigate(() => new WelcomePage());
        return;
      }

      await Transition(() => QuizVM.MovePrevious());
    }

    private async void BtnFirstChoice_Click(object sender, RoutedEventArgs e)
    {
      if (_isTransitioning)
      {
        return;
      }

      QuizVM.CurrentQuestion.SelectedIndex = 1;

      if (QuizVM.DisplayCurrentIndex == QuizVM.Quiz.Count)
      {
        await OnQuizFinished();
      }
      else
      {
        await Transition(() => QuizVM.MoveNext());
      }
    }

    private async void BtnSecondChoice_Click(object sender, RoutedEventArgs e)
    {
      if (_isTransitioning)
      {
        return;
      }

      QuizVM.CurrentQuestion.SelectedIndex = 2;

      if (QuizVM.DisplayCurrentIndex == QuizVM.Quiz.Count)
      {
        await OnQuizFinished();
      }
      else
      {
        await Transition(() => QuizVM.MoveNext());
      }
    }

    private async void HomeButton_Click(object sender, RoutedEventArgs e)
    {
      await App.Current.MainWindow.Navigate(() => new WelcomePage());
    }

    private async Task OnQuizFinished()
    {
      var instruction3 = (string)Application.Current.Resources["SInstruction3"];
      var instruction4 = (string)Application.Current.Resources["SInstruction4"];

      if (instruction3 == null
        || instruction4 == null)
      {
        throw new InvalidOperationException(
          "Unable to get resource dictionary for instructions.");
      }

      await App.Current.MainWindow.Navigate(() => new InstructionPage()
        .DisplayMessages(
          instruction3,
          instruction4)
        .ThenShowPage(() => new ResultPage(new Mbti(QuizVM.Quiz))));
    }

    private async Task Transition(Action onFadedOut)
    {
      _isTransitioning = true;
      App.Current.FadeOutStoryboard.Begin(QuizArea);
      await Task.Delay(400);
      onFadedOut();
      App.Current.FadeInStoryboard.Begin(QuizArea);
      _isTransitioning = false;
    }
  }
}