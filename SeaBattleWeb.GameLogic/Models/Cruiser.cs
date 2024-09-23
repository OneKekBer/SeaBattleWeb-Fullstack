
using SeaBattleWeb.GameLogic.Models.Abstracts;

namespace SeaBattleWeb.GameLogic.Models
{
    internal class Cruiser : Ship
    {
        public const int cruiserSize = 4;
        public Cruiser() : base(cruiserSize, "Cruiser")
        {

        }
    }
}
