using System;

namespace MBTI.Models
{
  public struct PersonalityType
  {
    public PersonalityPrefixes1 Prefix1 { get; set; }
    public PersonalityPrefixes2 Prefix2 { get; set; }
    public PersonalityPrefixes3 Prefix3 { get; set; }
    public PersonalityPrefixes4 Prefix4 { get; set; }

    public static bool operator !=(PersonalityType p1, PersonalityType p2)
    {
      return !(p1 == p2);
    }

    public static bool operator ==(PersonalityType p1, PersonalityType p2)
    {
      return p1.Prefix1 == p2.Prefix1
        && p1.Prefix2 == p2.Prefix2
        && p1.Prefix3 == p2.Prefix3
        && p1.Prefix4 == p2.Prefix4;
    }

    public static PersonalityType Parse(string acronym)
    {
      if (string.IsNullOrWhiteSpace(acronym))
      {
        throw new ArgumentException("Cannot be empty.", nameof(acronym));
      }

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

    public PersonalityType CloneAndSet(
      PersonalityPrefixes1? prefix1 = null,
      PersonalityPrefixes2? prefix2 = null,
      PersonalityPrefixes3? prefix3 = null,
      PersonalityPrefixes4? prefix4 = null)
    {
      return new PersonalityType()
      {
        Prefix1 = prefix1 ?? Prefix1,
        Prefix2 = prefix2 ?? Prefix2,
        Prefix3 = prefix3 ?? Prefix3,
        Prefix4 = prefix4 ?? Prefix4,
      };
    }

    public override bool Equals(object obj)
    {
      return obj is PersonalityType type
        && Prefix1 == type.Prefix1
        && Prefix2 == type.Prefix2
        && Prefix3 == type.Prefix3
        && Prefix4 == type.Prefix4;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Prefix1, Prefix2, Prefix3, Prefix4);
    }
  }
}