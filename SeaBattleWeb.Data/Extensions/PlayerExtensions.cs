using SeaBattleWeb.Data.Entities;

namespace SeaBattleWeb.Data.Extensions;

public static class PlayerExtensions
{
    public static bool IsNotEmpty(this Player player)
    {
        return player.PLayerId == Guid.Empty ? false : true;
    }
}