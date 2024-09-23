//using SeaBattle.Values;
//using SeaBattleWeb.Data.Entities;
//using SeaBattleWeb.Data.GameLogic.Models.Board;

//namespace SeaBattleWeb.Data.GameLogic.Components
//{
//    public class Engine
//    {
//        private Board _board;
//        private IInput _inputHandler;
//        private IOutput _outputHandler;

//        public Engine(Board board, IInput inputHandler, IOutput outputHandler)
//        {
//            _board = board;
//            _inputHandler = inputHandler;
//            _outputHandler = outputHandler;
//        }

//        private void HitShip(Panel currentPanel, Coordinates userCoords)
//        {
//            var ship = currentPanel.Ship;
//            Console.WriteLine($"You hit {ship.Name}");
//            ship.AddHit();

//            if (ship.IsDestroyed)
//            {
//                Console.WriteLine($"Ship {ship.Name} destroyed!!");
//            }
//            currentPanel.RegisterShot();
//        }

//        public PanelState ShootToTtile(Coordinates coords)
//        {
//            var currentPanel = _board[coords];

//            if (currentPanel.PanelState == PanelState.ContainsShip)
//            {
//                HitShip(currentPanel, coords);
//                return PanelState.Shooted;
//            }
//            else if (currentPanel.PanelState == PanelState.Empty)
//            {

//                Console.WriteLine("Miss");
//                currentPanel.RegisterShot();
//                return PanelState.Miss;
//            }
//            else
//            {
//                Console.WriteLine("You already shot at this tile!");
//                return currentPanel.PanelState; //potential error place
//            }
//        }

//        public void DisplayBoard()
//        {
//            _outputHandler.DisplayBoard();
//        }

//        public async Task<PanelState> HandleSendingCoordiantesAsync()
//        {
//            Coordinates userCoords = await _inputHandler.GetCoordinatesAsync();
//            PanelState panelStateAfterShoot = ShootToTtile(userCoords);
//            return panelStateAfterShoot;
//        }

//        public void Start()
//        {
//            _board.FillBoard();
//            PlaceShips();
//        }

//        public void PlaceShips()
//        {
//            ShipPlacer shipPlacer = new ShipPlacer(_board);

//            shipPlacer.PlaceShip(new Cruiser());
//            shipPlacer.PlaceShip(new Cruiser());
//            shipPlacer.PlaceShip(new Cruiser());
//        }
//    }
//}
