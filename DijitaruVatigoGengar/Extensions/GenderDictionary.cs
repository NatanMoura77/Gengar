using DijitaruVatigoGengar.Enums;

namespace DijitaruVatigoGengar.Extensions;

public static class GenderDictionary
{
    private static Dictionary<string, Gender> map = new Dictionary<string, Gender>
    {
        {"masculino", Gender.Masculino },
        {"feminino", Gender.Feminino }
    };
    public static string GenderToString(this Gender type)
    {
        return map.First(c => c.Value == type).Key;
    }

    public static Gender GenderToValue(this string text)
    {
        return map.First(c => c.Key == text).Value;
    }
}
