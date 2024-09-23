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
        public PanelState PanelState { get; private set; } = PanelState.Empty;

        public Ship Ship { get; set; }

        public void PlaceShip(Ship ship)
        {
            if (Ship is not null)
            {
                throw new Exception("");
                //return;
            }
            PanelState = PanelState.ContainsShip;
            Ship = ship;
        }

        public void RegisterShot()
        {
            if (Ship is not null)
                PanelState = PanelState.Shooted;
            else
                PanelState = PanelState.Miss;
        }
    }
}
