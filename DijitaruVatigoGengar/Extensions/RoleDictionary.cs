using DijitaruVatigoGengar.Enums;

namespace DijitaruVatigoGengar.Extensions;

public static class RoleDictionary
{
    private static Dictionary<string, Role> map = new Dictionary<string, Role>
    {
        {"normal", Role.Normal},
        {"approver", Role.Approver},
        {"admin", Role.Admin}
    };
    public static string RoleToString(this Role role)
    {
        return map.First(c => c.Value == role).Key;
    }

    public static Role RoleToValue(this string text)
    {
        return map.First(c => c.Key == text).Value;
    }
}
