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

using MBTI.Logic;
using MBTI.ViewModels;

namespace MBTI
{
  /// <summary>
  /// Interaction logic for ResultPage.xaml
  /// </summary>
  public partial class ResultPage : Page
  {
    public ResultPage()
    {
      InitializeComponent();
    }

    public ResultPage(Mbti mbti) : this()
    {
      MbtiVM = new MbtiVM(mbti);
      DataContext = MbtiVM;
    }

    public MbtiVM MbtiVM { get; }

    private async void BtnStudy_Click(object sender, RoutedEventArgs e)
    {
      await App.MainWindow.Navigate(new StudyPage(this));
    }
  }
}