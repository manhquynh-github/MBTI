﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

using MBTI.Logic;
using MBTI.Models;
using MBTI.WindowsGUI.ViewModels;

namespace MBTI.WindowsGUI
{
  /// <summary>
  /// Interaction logic for StudyPage.xaml
  /// </summary>
  public partial class StudyPage : Page
  {
    private readonly ResultPage _resultPage;
    private readonly List<(TextBlock Tbl, Func<string> PrefixFunc)> _tblPrefixes;
    private readonly List<FrameworkElement> _transitionElements;

    public StudyPage(Mbti mbti) : this(mbti.GetFinalType())
    {
    }

    public StudyPage(ResultPage resultPage) : this(resultPage.MbtiVM.Mbti)
    {
      _resultPage = resultPage;
    }

    public StudyPage() : this(
      new PersonalityType()
      {
        Prefix1 = PersonalityPrefixes1.Extrovert,
        Prefix2 = PersonalityPrefixes2.Sensing,
        Prefix3 = PersonalityPrefixes3.Thinking,
        Prefix4 = PersonalityPrefixes4.Judging,
      })
    {
    }

    public StudyPage(PersonalityType type)
    {
      InitializeComponent();
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
      if (_resultPage != null)
      {
        await App.Current.MainWindow.Navigate(_resultPage);
      }
      else
      {
        await App.Current.MainWindow.Navigate(() => new WelcomePage());
      }
    }

    private async void HomeButton_Click(object sender, RoutedEventArgs e)
    {
      await App.Current.MainWindow.Navigate(() => new WelcomePage());
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
        App.Current.FadeOutStoryboard.Begin(element);
      }
      await Task.Delay(400);

      if (!(onFadedOut is null))
      {
        await Dispatcher.InvokeAsync(onFadedOut);
      }

      foreach (FrameworkElement element in _transitionElements)
      {
        App.Current.FadeInStoryboard.Begin(element);
      }
    }

    private void UpdatePrefixes()
    {
      var specialPrefix2 = StudyVM.PersonalityType.Prefix2 == PersonalityPrefixes2.Intuition;

      for (var i = 0; i < _tblPrefixes.Count; i++)
      {
        if (i == 1
          && specialPrefix2)
        {
          continue;
        }

        (TextBlock Tbl, Func<string> PrefixFunc) = _tblPrefixes[i];
        Tbl.Inlines.Clear();
        var prefix = PrefixFunc();
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
      var prefix2 = _tblPrefixes[1].PrefixFunc();
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