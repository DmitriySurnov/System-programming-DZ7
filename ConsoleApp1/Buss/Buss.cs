using BusesInTown.Routes;
using ConsoleApp1;
using System;
using System.Threading;

namespace BusesInTown.Buss
{
    internal class Bus
    {
        private readonly string _name;
        private readonly BaseRoute _route;
        private bool _direction;
        private int _stopsIndex;
        private bool _stop;
        private bool _answer;
        private readonly Action<string> _action;

        public Bus(int busNumber, BaseRoute route, Action<string> action, bool direction = true)
        {
            _name = $"Bus {busNumber}";
            _route = route;
            _direction = direction;
            _stopsIndex = _direction ? 0 : _route.Count();
            _stop = false;
            _answer = false;
            _action = action;
        }

        private void Answer() { 
            _answer = true;
        }

        public void Start()
        {
            while (!_stop)
            {
                int numberMinutesToStop = _route.GetStationToStationTime(_stopsIndex);
                if (numberMinutesToStop == -1)
                    throw new ArgumentOutOfRangeException(
                        $"Can not create route with number {_stopsIndex}");
                Program.TownWatchProxy.Invoke(() =>Program.townWatchs.Add(Answer, numberMinutesToStop));
                while (true)
                {
                    if (_answer)
                    {
                        _answer = false;
                        _stopsIndex = _direction ? _stopsIndex+1 : _stopsIndex-1;
                        if (_stopsIndex == -1)
                        {
                            _direction = true;
                            _stopsIndex+=2;
                        }
                        if (_stopsIndex > _route.Count())
                        {
                            _direction = false;
                            _stopsIndex-=2;
                        }
                        _action($"{_name}  {_route.GetStations(_stopsIndex)}");
                        break;
                    }
                    else
                        Thread.Sleep(1000);
                }
            }
        }
        public void Stop()
        {
            _stop = true;
        }
    }
}
