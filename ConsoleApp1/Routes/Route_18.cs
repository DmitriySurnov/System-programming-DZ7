using BusesInTown.Stations;

namespace BusesInTown.Routes
{
    internal class Route_18 : BaseRoute
    {
        public override int RouteNumber => 18;

        public Route_18() : base()
        {
            StationToStationTime[0] = 7;
            StationToStationTime[StationToStationTime.Length - 1] = 25;
            Stations[0] = new Station("Start 18");
            Stations[Stations.Length - 1] = new Station("End 18");

        }

    }
}
