using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MBTI.Logic;
using MBTI.Models;
using MBTI.ViewModels;

namespace MBTI
{
  /// <summary>
  /// Interaction logic for QuizPage.xaml
  /// </summary>
  public partial class QuizPage : Page
  {
    private readonly Storyboard _fadeInStoryboard;
    private readonly Storyboard _fadeOutStoryboard;

    public QuizPage()
    {
      InitializeComponent();
      _fadeInStoryboard = (Storyboard)Application.Current.Resources["FadeInStoryboard"];
      _fadeOutStoryboard = (Storyboard)Application.Current.Resources["FadeOutStoryboard"];

      string[] questions = (string[])Application.Current.Resources["Questions.vi"];
      if (questions == null || questions.Length == 0)
      {
        throw new InvalidOperationException();
      }

      Parser.Parse(questions, out IList<Question> quiz);
      QuizVM = new QuizVM(quiz);
      DataContext = QuizVM;
    }

    public QuizVM QuizVM { get; }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
      if (QuizVM.CurrentIndex == 0)
      {
        await App.MainWindow.Navigate(new WelcomePage());
        return;
      }

      await Transition(() => QuizVM.MovePrevious());
    }

    private async void BtnFirstChoice_Click(object sender, RoutedEventArgs e)
    {
      QuizVM.CurrentQuestion.SelectedIndex = 1;

      if (QuizVM.DisplayCurrentIndex == QuizVM.Quiz.Count)
      {
        await App.MainWindow.Navigate(new InstructionPage(
          new ResultPage(new Mbti(QuizVM.Quiz)),
          "Bạn đã hoàn thành trắc nghiệm",
          "Tính cách đại diện của bạn là"));
      }
      else
      {
        await Transition(() => QuizVM.MoveNext());
      }
    }

    private async void BtnSecondChoice_Click(object sender, RoutedEventArgs e)
    {
      QuizVM.CurrentQuestion.SelectedIndex = 2;

      if (QuizVM.DisplayCurrentIndex == QuizVM.Quiz.Count)
      {
        await App.MainWindow.Navigate(new InstructionPage(
          new StudyPage(),
          "Bạn đã hoàn thành trắc nghiệm",
          "Tính cách đại diện của bạn là"));
      }
      else
      {
        await Transition(() => QuizVM.MoveNext());
      }
    }

    private async void HomeButton_Click(object sender, RoutedEventArgs e)
    {
      await App.MainWindow.Navigate(new WelcomePage());
    }

    private async Task Transition(Action onFadedOut)
    {
      _fadeOutStoryboard.Begin(QuizArea);
      await Task.Delay(400);
      onFadedOut();
      _fadeInStoryboard.Begin(QuizArea);
    }
  }
}