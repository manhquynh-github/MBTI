using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using MBTI.WindowsGUI.ViewModels;

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
      CbbLanguage.SelectedItem = App.Current.Language;
    }

    private async void AboutButton_Click(object sender, RoutedEventArgs e)
    {
      await App.Current.MainWindow.Navigate(() => new AboutPage());
    }

    private async void BtnQuiz_Click(object sender, RoutedEventArgs e)
    {
      var instruction1 = (string)ResourcesVM.Instance["SInstruction1"];
      var instruction2 = (string)ResourcesVM.Instance["SInstruction2"];

      if (instruction1 == null
       || instruction2 == null)
      {
        throw new InvalidOperationException(
          "Unable to get resource dictionary for instructions.");
      }

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
          App.Current.Language = (CultureInfo)e.AddedItems[0];
          return;
        }

        App.Current.FadeOutStoryboard.Begin(ContentArea);
        await Task.Delay(TimeSpan.FromMilliseconds(400));
        await Task.Run(() => App.Current.Language = (CultureInfo)e.AddedItems[0]);
        App.Current.FadeInStoryboard.Begin(ContentArea);
      }
    }
  }
}