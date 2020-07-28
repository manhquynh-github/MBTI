using System.Text.Json.Serialization;

namespace MBTI.Models
{
  public class Question
  {
    [JsonPropertyName("Question")]
    public string Content { get; set; }

    public string FirstChoice { get; set; }
    public string SecondChoice { get; set; }

    [JsonIgnore]
    public int SelectedIndex { get; set; }
  }
}