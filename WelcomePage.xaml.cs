using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace MBTI
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

      await App.Current.MainWindow.Navigate(new InstructionPage(
        new QuizPage(),
        instruction1,
        instruction2));
    }

    private async void BtnStudy_Click(object sender, RoutedEventArgs e)
    {
      await App.Current.MainWindow.Navigate(new StudyPage());
    }

    private void CbbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (e.AddedItems.Count == 1)
      {
        App.Current.UpdateLanguage((CultureInfo)e.AddedItems[0]);
      }
    }
  }
}