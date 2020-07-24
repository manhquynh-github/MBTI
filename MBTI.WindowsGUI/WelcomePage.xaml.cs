using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MBTI.WindowsGUI
{
  /// <summary>
  /// Interaction logic for WelcomePage.xaml
  /// </summary>
  public partial class WelcomePage : Page
  {
    public WelcomePage()
    {
      InitializeComponent();

      CbbLanguage.ItemsSource = App.Current.SupportedLanguages;
    }

    private async void BtnQuiz_Click(object sender, RoutedEventArgs e)
    {
      string instruction1 = (string)Application.Current.Resources["SInstruction1"];
      string instruction2 = (string)Application.Current.Resources["SInstruction2"];

      await App.Current.MainWindow.Navigate(() => new InstructionPage()
        .DisplayMessages(
          instruction1,
          instruction2)
        .ThenShowPage(() => new QuizPage()));
    }

    private async void BtnStudy_Click(object sender, RoutedEventArgs e)
    {
      await App.Current.MainWindow.Navigate(() => new StudyPage());
    }

    private async void CbbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (e.AddedItems.Count == 1)
      {
        if (!IsLoaded)
        {
          App.Current.UpdateLanguage((CultureInfo)e.AddedItems[0]);
          return;
        }

        App.Current.FadeOutStoryboard.Begin(ContentArea);
        await Task.Delay(TimeSpan.FromMilliseconds(400));
        await Task.Run(() => App.Current.UpdateLanguage((CultureInfo)e.AddedItems[0]));
        App.Current.FadeInStoryboard.Begin(ContentArea);
      }
    }
  }
}