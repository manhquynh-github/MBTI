using MBTI.ConsoleApp.Screens;
using MBTI.Resources.Properties;

namespace MBTI.ConsoleApp.Commands
{
  public class BackCommand : Command
  {
    public BackCommand(string description, ScreenBase toScreen) : base(
      description,
      () => toScreen)
    {
    }

    public BackCommand(ScreenBase toScreen) : this(
      Content.SBack,
      toScreen)
    {
    }
  }
}