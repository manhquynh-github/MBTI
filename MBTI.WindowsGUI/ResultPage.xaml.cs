using System.Windows;
using System.Windows.Controls;

using MBTI.Logic;
using MBTI.WindowsGUI.ViewModels;

namespace MBTI.WindowsGUI
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
      await App.Current.MainWindow.Navigate(() => new StudyPage(this));
    }
  }
}