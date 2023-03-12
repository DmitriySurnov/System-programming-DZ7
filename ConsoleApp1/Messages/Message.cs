using BusesInTown.Buses;

namespace BusesInTown.Messages
{
    internal class Message
    {
        private Buse _buse;
        private int _numberMinutesToStop;

        public Message(Buse buse, int numberMinutesToStop)
        {
            _buse = buse;
            _numberMinutesToStop = numberMinutesToStop;
        }

        public Buse GetBuse() =>_buse;

        public int GetNumberMinutesToStop() => _numberMinutesToStop;

        public static int SortInAscending(Message x, Message y)
        {
            if (x._numberMinutesToStop > y._numberMinutesToStop)
                return 0;
            else
                return -1;
        }

        public static Message operator -(Message counter1, Message counter2)
        {
            int numberMinutesToStop = counter1._numberMinutesToStop - counter2._numberMinutesToStop;
            return new Message(counter1._buse, numberMinutesToStop);
        }

        public static bool operator <(Message counter1, Message counter2)
        {
            return counter1._numberMinutesToStop < counter2._numberMinutesToStop;
        }

        public static bool operator >(Message counter1, Message counter2)
        {
            return counter1._numberMinutesToStop > counter2._numberMinutesToStop;
        }
    }
}
