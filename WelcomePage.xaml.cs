using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }

    private async void BtnQuiz_Click(object sender, RoutedEventArgs e)
    {
      string instruction1 = (string)Application.Current.Resources["SInstruction1"];
      string instruction2 = (string)Application.Current.Resources["SInstruction2"];

      await App.MainWindow.Navigate(new InstructionPage(
        new QuizPage(),
        instruction1,
        instruction2));
    }

    private async void BtnStudy_Click(object sender, RoutedEventArgs e)
    {
      await App.MainWindow.Navigate(new StudyPage());
    }
  }
}