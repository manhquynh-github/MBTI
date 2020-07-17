using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MBTI.ViewModels
{
  public abstract class VMBase :
    INotifyPropertyChanging,
    INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    public event PropertyChangingEventHandler PropertyChanging;

    public void NotifyPropertyChanged(
      [CallerMemberName] string propertyName = null)
    {
      if (propertyName != null)
      {
        PropertyChanged?.Invoke(
          this, new PropertyChangedEventArgs(propertyName));
      }
    }

    public void NotifyPropertyChanging(
       [CallerMemberName] string propertyName = null)
    {
      if (propertyName != null)
      {
        PropertyChanging?.Invoke(
          this, new PropertyChangingEventArgs(propertyName));
      }
    }

    protected void SetProperty<T>(
      ref T field,
      T newValue,
      [CallerMemberName] string propertyName = null)
    {
      if (propertyName != null)
      {
        if (!EqualityComparer<T>.Default.Equals(field, newValue))
        {
          PropertyChanging?.Invoke(
            this, new PropertyChangingEventArgs(propertyName));
          field = newValue;
          PropertyChanged?.Invoke(
            this, new PropertyChangedEventArgs(propertyName));
        }
      }
    }
  }
}