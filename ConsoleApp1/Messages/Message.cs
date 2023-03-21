using System;

namespace BusesInTown.Messages
{
    internal class Message
    {
        private readonly Action _action;
        private readonly int _numberMinutesToStop;
        public Action Action => _action;
        public int GetNumberMinutesToStop() => _numberMinutesToStop;
        public Message(Action action, int numberMinutesToStop)
        {
            _action = action;
            _numberMinutesToStop = numberMinutesToStop;
        }
        public static Message operator -(Message counter1, Message counter2)
        {
            int numberMinutesToStop = counter1._numberMinutesToStop - counter2._numberMinutesToStop;
            return new Message(counter1._action, numberMinutesToStop);
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
