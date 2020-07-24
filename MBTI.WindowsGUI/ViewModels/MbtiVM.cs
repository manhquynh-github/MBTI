using MBTI.Logic;

namespace MBTI.WindowsGUI.ViewModels
{
  public class MbtiVM : VMBase
  {
    public MbtiVM(Mbti mbti)
    {
      Mbti = mbti;
    }

    public Mbti Mbti { get; }

    public double PercentExtrovert => Mbti.Extrovert / 20.0;
    public double PercentFeeling => Mbti.Feeling / 20.0;
    public double PercentIntrovert => Mbti.Introvert / 20.0;
    public double PercentIntuition => Mbti.Intuition / 20.0;
    public double PercentJudging => Mbti.Judging / 20.0;
    public double PercentPerceptive => Mbti.Perceptive / 20.0;
    public double PercentSensing => Mbti.Sensing / 20.0;
    public double PercentThinking => Mbti.Thinking / 20.0;
    public string TypeAcronym => Mbti.GetFinalType().GetAcronym();
  }
}