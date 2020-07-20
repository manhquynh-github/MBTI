using System;
using System.Collections.Generic;
using System.ComponentModel;
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
  /// Interaction logic for StudyPage.xaml
  /// </summary>
  public partial class StudyPage : Page
  {
    private readonly Storyboard _fadeInStoryboard;
    private readonly Storyboard _fadeOutStoryboard;

    public StudyPage() : this(
      new PersonalityType()
      {
        Prefix1 = PersonalityPrefixes1.Introvert,
        Prefix2 = PersonalityPrefixes2.Sensing,
        Prefix3 = PersonalityPrefixes3.Thinking,
        Prefix4 = PersonalityPrefixes4.Judging,
      })
    {
    }

    public StudyPage(PersonalityType type)
    {
      InitializeComponent();
      _fadeInStoryboard = (Storyboard)Application.Current.Resources["FadeInStoryboard"];
      _fadeOutStoryboard = (Storyboard)Application.Current.Resources["FadeOutStoryboard"];

      StudyVM = new StudyVM(type);
      StudyVM.RefreshUI();
      StudyVM.OnNeedsRefreshUI += StudyVM_OnNeedsRefreshUI;
      DataContext = StudyVM;
    }

    public StudyVM StudyVM { get; }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
      await App.MainWindow.Navigate(new WelcomePage());
    }

    private async void StudyVM_OnNeedsRefreshUI(object sender, EventArgs e)
    {
      await Transition(() => StudyVM.RefreshUI());
    }

    private async Task Transition(Action onFadedOut)
    {
      _fadeOutStoryboard.Begin(TblAcronym);
      _fadeOutStoryboard.Begin(TblPrefix1);
      _fadeOutStoryboard.Begin(TblPrefix2);
      _fadeOutStoryboard.Begin(TblPrefix3);
      _fadeOutStoryboard.Begin(TblPrefix4);
      _fadeOutStoryboard.Begin(DescriptionArea);
      _fadeOutStoryboard.Begin(JobsArea);
      await Task.Delay(400);
      onFadedOut?.Invoke();
      _fadeInStoryboard.Begin(TblAcronym);
      _fadeInStoryboard.Begin(TblPrefix1);
      _fadeInStoryboard.Begin(TblPrefix2);
      _fadeInStoryboard.Begin(TblPrefix3);
      _fadeInStoryboard.Begin(TblPrefix4);
      _fadeInStoryboard.Begin(DescriptionArea);
      _fadeInStoryboard.Begin(JobsArea);
    }
  }
}