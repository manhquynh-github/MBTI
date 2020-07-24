using MBTI.Models;

namespace MBTI.Logic
{
  public static class Extensions
  {
    public static string GetAcronym(this PersonalityPrefixes1 type)
    {
      return type.ToString().Substring(0, 1);
    }

    public static string GetAcronym(this PersonalityPrefixes2 type)
    {
      return type == PersonalityPrefixes2.Sensing
        ? type.ToString().Substring(0, 1)
        : "N";
    }

    public static string GetAcronym(this PersonalityPrefixes3 type)
    {
      return type.ToString().Substring(0, 1);
    }

    public static string GetAcronym(this PersonalityPrefixes4 type)
    {
      return type.ToString().Substring(0, 1);
    }

    public static string GetAcronym(this PersonalityType type)
    {
      return type.Prefix1.GetAcronym()
        + type.Prefix2.GetAcronym()
        + type.Prefix3.GetAcronym()
        + type.Prefix4.GetAcronym();
    }
  }
}