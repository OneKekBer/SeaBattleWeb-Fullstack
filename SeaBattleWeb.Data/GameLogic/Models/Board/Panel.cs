using SeaBattleWeb.Data.Entities;
using SeaBattleWeb.GameLogic.Models.Abstracts;

namespace SeaBattleWeb.Data.GameLogic.Models.Board
{
    public enum PanelState
    {
        Empty = 0,
        ContainsShip = 1,
        Shooted = 2,
        Miss = 3
    }

    public class Panel
    {
        public PanelState PanelState { get; set; } = PanelState.Empty;

        public void PlaceShip(Ship ship)
        {
            PanelState = PanelState.ContainsShip;
        }

        public void RegisterShot()
        {
            if (PanelState == PanelState.ContainsShip)
                PanelState = PanelState.Shooted;
            else
                PanelState = PanelState.Miss;
        }
    }
}
