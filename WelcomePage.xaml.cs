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
      await App.MainWindow.Navigate(new InstructionPage(
        new QuizPage(),
        "Chọn câu mô tả rõ nét về bạn nhất",
        "Hãy thành thật với chính mình"));
    }

    private async void BtnStudy_Click(object sender, RoutedEventArgs e)
    {
      await App.MainWindow.Navigate(new StudyPage());
    }
  }
}