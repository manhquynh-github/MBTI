using System;
using System.Collections.Generic;
using System.Text;
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
      DataContext = StudyVM;
    }

    public StudyVM StudyVM { get; }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
      await App.MainWindow.Navigate(new WelcomePage());
    }
  }
}