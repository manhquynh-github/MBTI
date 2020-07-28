using System;
using System.Collections.Generic;
using System.Linq;

using MBTI.Logic;
using MBTI.Models;
using MBTI.Resources;

namespace MBTI.WindowsGUI.ViewModels
{
  public class StudyVM : VMBase
  {
    private readonly Dictionary<string, PersonalityTypeDescription> _descriptions;
    private PersonalityTypeDescription _content;
    private bool _needsRefreshUI;
    private PersonalityType _personalityType;

    public StudyVM(PersonalityType type)
    {
      _descriptions = Helper.GetDescriptions();
      PersonalityType = type;
      NeedsRefreshUI = true;
    }

    public event EventHandler OnNeedsRefreshUI;

    public PersonalityTypeDescription Content
    {
      get => _content;
      private set => SetProperty(ref _content, value);
    }

    public string DisplayDescriptions => string.Join(
      Environment.NewLine,
      Content.Descriptions.Select(v => $"- {v}."));

    public string DisplayPrefix1 => PersonalityType.Prefix1.ToString().ToUpper();
    public string DisplayPrefix2 => PersonalityType.Prefix2.ToString().ToUpper();
    public string DisplayPrefix3 => PersonalityType.Prefix3.ToString().ToUpper();
    public string DisplayPrefix4 => PersonalityType.Prefix4.ToString().ToUpper();

    public string DisplaySuggestedJobs => string.Join(
      Environment.NewLine,
      Content.SuggestedJobs.Select(v => $"- {v}."));

    public bool NeedsRefreshUI
    {
      get => _needsRefreshUI;
      private set
      {
        if (_needsRefreshUI != value)
        {
          _needsRefreshUI = value;

          if (_needsRefreshUI == true)
          {
            OnNeedsRefreshUI?.Invoke(this, EventArgs.Empty);
          }
        }
      }
    }

    public PersonalityType PersonalityType
    {
      get => _personalityType;
      set
      {
        SetProperty(ref _personalityType, value);
        NeedsRefreshUI = true;
      }
    }

    public int SelectedPrefix1
    {
      get => (int)PersonalityType.Prefix1;
      set
      {
        if (value < 0 || value > 1)
        {
          throw new IndexOutOfRangeException();
        }

        PersonalityType = PersonalityType
          .CloneAndSet(prefix1: (PersonalityPrefixes1)value);
      }
    }

    public int SelectedPrefix2
    {
      get => (int)PersonalityType.Prefix2;
      set
      {
        if (value < 0 || value > 1)
        {
          throw new IndexOutOfRangeException();
        }

        PersonalityType = PersonalityType
          .CloneAndSet(prefix2: (PersonalityPrefixes2)value);
      }
    }

    public int SelectedPrefix3
    {
      get => (int)PersonalityType.Prefix3;
      set
      {
        if (value < 0 || value > 1)
        {
          throw new IndexOutOfRangeException();
        }

        PersonalityType = PersonalityType
          .CloneAndSet(prefix3: (PersonalityPrefixes3)value);
      }
    }

    public int SelectedPrefix4
    {
      get => (int)PersonalityType.Prefix4;
      set
      {
        if (value < 0 || value > 1)
        {
          throw new IndexOutOfRangeException();
        }

        PersonalityType = PersonalityType
          .CloneAndSet(prefix4: (PersonalityPrefixes4)value);
      }
    }

    public string TypeAcronym => PersonalityType.GetAcronym();

    public void RefreshUI()
    {
      if (!NeedsRefreshUI)
      {
        return;
      }

      NotifyPropertyChanged(nameof(TypeAcronym));
      Content = _descriptions[TypeAcronym];
      NotifyPropertyChanged(nameof(DisplayDescriptions));
      NotifyPropertyChanged(nameof(DisplaySuggestedJobs));
      NotifyPropertyChanged(nameof(DisplayPrefix1));
      NotifyPropertyChanged(nameof(DisplayPrefix2));
      NotifyPropertyChanged(nameof(DisplayPrefix3));
      NotifyPropertyChanged(nameof(DisplayPrefix4));
      NotifyPropertyChanged(nameof(SelectedPrefix1));
      NotifyPropertyChanged(nameof(SelectedPrefix2));
      NotifyPropertyChanged(nameof(SelectedPrefix3));
      NotifyPropertyChanged(nameof(SelectedPrefix4));
      NeedsRefreshUI = false;
    }
  }
}