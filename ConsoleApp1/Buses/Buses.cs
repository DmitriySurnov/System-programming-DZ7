using BusesInTown.Routes;
using BusesInTown.TownWatchs;
using ConsoleApp1;
using System;

namespace BusesInTown.Buses
{
    internal class Buse
    {
        private string _name;
        private BaseRoute _routes;
        private bool _direction;
        private int _stopsIndex;

        public Buse(int nomerBuse, bool direction = true)
        {
            _name = $"Bus {nomerBuse}";
            _routes = RouteFactory.CreateRoute(nomerBuse);
            _direction = direction;
            _stopsIndex = _direction ? 0 : _routes.Count();
        }

        public void star()
        {
            while (true)
            {
                int numberMinutesToStop = _routes.GetStationToStationTime(_stopsIndex);
                if (numberMinutesToStop == -1)
                    throw new ArgumentOutOfRangeException(
                        $"Can not create route with number {_stopsIndex}");
                Program.TownWatchProxy.Invoke(() => TownWatch.AddMs1(this, numberMinutesToStop));
                while (true)
                {
                    if (TownWatch.GetMs2(this))
                    {
                        _stopsIndex = _direction ? _stopsIndex+1 : _stopsIndex-1;
                        if (_stopsIndex == -1)
                        {
                            _direction = true;
                            _stopsIndex+=2;
                        }
                        if (_stopsIndex > _routes.Count())
                        {
                            _direction = false;
                            _stopsIndex-=2;
                        }
                        TownWatch.AddMessage($"{_name}  {_routes.GetStations(_stopsIndex)}");
                        break;
                    }
                }
            }
        }
    }
}
