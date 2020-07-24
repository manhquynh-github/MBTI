using System;

namespace MBTI.Models
{
  public struct PersonalityType
  {
    public PersonalityPrefixes1 Prefix1 { get; set; }
    public PersonalityPrefixes2 Prefix2 { get; set; }
    public PersonalityPrefixes3 Prefix3 { get; set; }
    public PersonalityPrefixes4 Prefix4 { get; set; }

    public static PersonalityType Parse(string acronym)
    {
      if (acronym.Length != 4)
      {
        throw new ArgumentException(nameof(acronym));
      }

      acronym = acronym.ToLower();

      return new PersonalityType()
      {
        Prefix1 = acronym[0] switch
        {
          'e' => PersonalityPrefixes1.Extrovert,
          'i' => PersonalityPrefixes1.Introvert,
          _ => throw new FormatException(nameof(acronym)),
        },
        Prefix2 = acronym[1] switch
        {
          's' => PersonalityPrefixes2.Sensing,
          'n' => PersonalityPrefixes2.Intuition,
          _ => throw new FormatException(nameof(acronym)),
        },
        Prefix3 = acronym[2] switch
        {
          't' => PersonalityPrefixes3.Thinking,
          'f' => PersonalityPrefixes3.Feeling,
          _ => throw new FormatException(nameof(acronym)),
        },
        Prefix4 = acronym[3] switch
        {
          'j' => PersonalityPrefixes4.Judging,
          'p' => PersonalityPrefixes4.Perceptive,
          _ => throw new FormatException(nameof(acronym)),
        }
      };
    }
  }
}