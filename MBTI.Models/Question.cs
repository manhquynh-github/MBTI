namespace MBTI.Models
{
  public class Question
  {
    public string Content { get; set; }
    public Choice FirstChoice { get; set; }
    public Choice SecondChoice { get; set; }
    public int SelectedIndex { get; set; }
  }
}