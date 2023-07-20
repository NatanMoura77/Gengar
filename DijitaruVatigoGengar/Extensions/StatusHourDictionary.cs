using DijitaruVatigoGengar.Enums;

namespace DijitaruVatigoGengar.Extensions;

public static class StatusHourDictionary
{
    private static Dictionary<string, StatusHour> map = new Dictionary<string, StatusHour>
    {
        {"approved", StatusHour.Approved},
        {"pending", StatusHour.Pending},
        {"rejected", StatusHour.Rejected}
    };
    public static string StatusToString(this StatusHour status)
    {
        return map.First(c => c.Value == status).Key;
    }

    public static StatusHour StatusToValue(this string text)
    {
        return map.First(c => c.Key == text).Value;
    }
}
