namespace BusesInTown.Stations
{
    internal class Station
    {
        public string StationName { get; }

        public Station(string stationName)
        {
            StationName = stationName;
        }

        public override string ToString()
        {
            return $"Station -> StationName = {StationName}";
        }
    }
}
