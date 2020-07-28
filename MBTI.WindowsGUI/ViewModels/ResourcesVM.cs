using System.Globalization;
using System.Resources;

using MBTI.Resources.Properties;

namespace MBTI.WindowsGUI.ViewModels
{
  public class ResourcesVM : VMBase
  {
    private readonly ResourceManager _resManager = Content.ResourceManager;
    public static ResourcesVM Instance { get; } = new ResourcesVM();

    public CultureInfo CurrentCulture
    {
      get => Content.Culture;
      set
      {
        if (Content.Culture != value)
        {
          Content.Culture = value;
          NotifyPropertyChanged(string.Empty);
        }
      }
    }

    public object this[string key] => _resManager.GetObject(key, Content.Culture);
  }
}