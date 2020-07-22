using System;
using System.Threading.Tasks;
using System.Windows;

using MBTI.Logic;
using MBTI.Models;

namespace MBTI.ViewModels
{
  public class StudyVM : VMBase
  {
    public Task OnPersonalityTypeChanging;
    private PersonalityTypeDescription _content;
    private PersonalityType _personalityType;

    public StudyVM(PersonalityType type)
    {
      PersonalityType = type;
      NeedsRefreshUI = true;
    }

    public event EventHandler OnNeedsRefreshUI;

    public PersonalityTypeDescription Content
    {
      get => _content;
      private set => SetProperty(ref _content, value);
    }

    public string DisplayPrefix1 => PersonalityType.Prefix1.ToString().ToUpper();
    public string DisplayPrefix2 => PersonalityType.Prefix2.ToString().ToUpper();
    public string DisplayPrefix3 => PersonalityType.Prefix3.ToString().ToUpper();
    public string DisplayPrefix4 => PersonalityType.Prefix4.ToString().ToUpper();
    public bool NeedsRefreshUI { get; private set; }

    public PersonalityType PersonalityType
    {
      get => _personalityType;
      set
      {
        SetProperty(ref _personalityType, value);
        NeedsRefreshUI = true;
        OnNeedsRefreshUI?.Invoke(this, EventArgs.Empty);
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

        PersonalityType = new PersonalityType()
        {
          Prefix1 = (PersonalityPrefixes1)value,
          Prefix2 = PersonalityType.Prefix2,
          Prefix3 = PersonalityType.Prefix3,
          Prefix4 = PersonalityType.Prefix4,
        };
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

        PersonalityType = new PersonalityType()
        {
          Prefix1 = PersonalityType.Prefix1,
          Prefix2 = (PersonalityPrefixes2)value,
          Prefix3 = PersonalityType.Prefix3,
          Prefix4 = PersonalityType.Prefix4,
        };
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

        PersonalityType = new PersonalityType()
        {
          Prefix1 = PersonalityType.Prefix1,
          Prefix2 = PersonalityType.Prefix2,
          Prefix3 = (PersonalityPrefixes3)value,
          Prefix4 = PersonalityType.Prefix4,
        };
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

        PersonalityType = new PersonalityType()
        {
          Prefix1 = PersonalityType.Prefix1,
          Prefix2 = PersonalityType.Prefix2,
          Prefix3 = PersonalityType.Prefix3,
          Prefix4 = (PersonalityPrefixes4)value,
        };
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
      UpdateDescription(PersonalityType);
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

    private void UpdateDescription(PersonalityType type)
    {
      string[] descriptionResource = (string[])Application.Current.Resources[TypeAcronym];
      if (descriptionResource == null || descriptionResource.Length == 0)
      {
        throw new InvalidOperationException();
      }

      Parser.Parse(descriptionResource, out PersonalityTypeDescription description);
      Content = description;
    }
  }
}