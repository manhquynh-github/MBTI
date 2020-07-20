using System;
using System.Collections.Generic;
using System.ComponentModel;
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
  /// Interaction logic for StudyPage.xaml
  /// </summary>
  public partial class StudyPage : Page
  {
    private readonly Storyboard _fadeInStoryboard;
    private readonly Storyboard _fadeOutStoryboard;
    private readonly List<(TextBlock Tbl, Func<string> PrefixFunc)> _tblPrefixes;
    private readonly List<FrameworkElement> _transitionElements;

    public StudyPage(Mbti mbti) : this(mbti.GetFinalType())
    {
    }

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
      _transitionElements = new List<FrameworkElement>()
      {
        TblAcronym,
        TblPrefix1,
        TblPrefix2,
        TblPrefix3,
        TblPrefix4,
        DescriptionArea,
        JobsArea,
      };
      _tblPrefixes = new List<(TextBlock, Func<string>)>()
      {
        (TblPrefix1, () => StudyVM.DisplayPrefix1 ),
        (TblPrefix2, () => StudyVM.DisplayPrefix2 ),
        (TblPrefix3, () => StudyVM.DisplayPrefix3 ),
        (TblPrefix4, () => StudyVM.DisplayPrefix4 ),
      };

      StudyVM = new StudyVM(type);
      StudyVM.RefreshUI();
      StudyVM.OnNeedsRefreshUI += StudyVM_OnNeedsRefreshUI;
      DataContext = StudyVM;

      UpdatePrefixes();
    }

    public StudyVM StudyVM { get; }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
      await App.MainWindow.Navigate(new WelcomePage());
    }

    private async void StudyVM_OnNeedsRefreshUI(object sender, EventArgs e)
    {
      await Transition(() => StudyVM.RefreshUI());
      UpdatePrefixes();
    }

    private async Task Transition(Action onFadedOut)
    {
      foreach (FrameworkElement element in _transitionElements)
      {
        _fadeOutStoryboard.Begin(element);
      }
      await Task.Delay(400);
      onFadedOut?.Invoke();
      foreach (FrameworkElement element in _transitionElements)
      {
        _fadeInStoryboard.Begin(element);
      }
    }

    private void UpdatePrefixes()
    {
      bool specialPrefix2 = StudyVM.SelectedPrefix2 == 1;

      for (int i = 0; i < _tblPrefixes.Count; i++)
      {
        if (i == 1
          && specialPrefix2)
        {
          continue;
        }

        (TextBlock Tbl, Func<string> PrefixFunc) = _tblPrefixes[i];
        Tbl.Inlines.Clear();
        string prefix = PrefixFunc();
        Tbl.Inlines.Add(new Run(prefix[0].ToString())
        {
          FontWeight = FontWeights.Bold,
          TextDecorations = TextDecorations.Underline,
        });
        Tbl.Inlines.Add(new Run(prefix.Substring(1)));
      }

      if (!specialPrefix2)
      {
        return;
      }

      TblPrefix2.Inlines.Clear();
      string prefix2 = _tblPrefixes[1].PrefixFunc();
      TblPrefix2.Inlines.Add(new Run(prefix2[0].ToString()));
      TblPrefix2.Inlines.Add(new Run(prefix2[1].ToString())
      {
        FontWeight = FontWeights.Bold,
        TextDecorations = TextDecorations.Underline,
      });
      TblPrefix2.Inlines.Add(new Run(prefix2.Substring(2)));
    }
  }
}