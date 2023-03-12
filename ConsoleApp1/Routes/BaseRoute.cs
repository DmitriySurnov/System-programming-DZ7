using BusesInTown.Stations;

namespace BusesInTown.Routes
{
    abstract class BaseRoute
    {
        public abstract int RouteNumber { get; }

        protected int[] StationToStationTime { get; }
        protected Station[] Stations { get; }

        public BaseRoute()
        {
            StationToStationTime = new[] { 0, 5, 10, 7, 0 };
            Stations = new Station[] { 
                null,// start
                new Station("station 1"),
                new Station("station 2"),
                new Station("station 3"),
                null,//finish
            };
        }
        public int Count()
        {
            return Stations.Length-1;
        }

        public int GetStationToStationTime(int id)
        {
            if (id < StationToStationTime.Length && id > -1)
                return StationToStationTime[id];
            return -1;
        }
        public string GetStations(int id)
        {
            if (id < Stations.Length && id > -1)
                return Stations[id].ToString();
            return "";
        }
    }
}
