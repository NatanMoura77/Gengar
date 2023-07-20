using DijitaruVatigoGengar.Enums;

namespace DijitaruVatigoGengar.Extensions;

public static class ProjectTypeDictionary
{
    private static Dictionary<string, ProjectType> map = new Dictionary<string, ProjectType>
    {
        {"squadService", ProjectType.SquadService },
        {"outsourcing", ProjectType.Outsourcing }
    };
    public static string ProjectToString(this ProjectType type)
    {
        return map.First(c => c.Value == type).Key;
    }

    public static ProjectType ProjectToValue(this string text)
    {
        return map.First(c => c.Key == text).Value;
    }
}
