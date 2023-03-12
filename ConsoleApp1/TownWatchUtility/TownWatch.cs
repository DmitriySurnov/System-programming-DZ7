using BusesInTown.Buses;
using BusesInTown.Messages;
using System.Collections.Generic;
using System.Threading;

namespace BusesInTown.TownWatchs
{
    internal static class TownWatch
    {
        private static List<Message> _ms1 = new List<Message>();
        private static List<Buse> _ms2 = new List<Buse>();
        private static List<string> _message = new List<string>();
        static private readonly object _ms1LockObj = new object();
        static private readonly object _ms2LockObj = new object();
        static private readonly object _messageLockObj = new object();
        public static bool Stop = false;

        public static void AddMs1(Buse buse, int numberMinutesToStop)
        {
            lock (_ms1LockObj)
            {
                _ms1.Add(new Message(buse, numberMinutesToStop));
            }
        }

        public static void AddMessage(string tet)
        {
            lock (_messageLockObj)
            {
                _message.Add(tet);
            }
        }

        public static string GetMessage( )
        {
            string temp = "";
            lock (_messageLockObj)
            {
                if (_message.Count > 0)
                {
                    temp = _message[0];
                    _message.RemoveAt(0);
                }
            }
            return temp;
        }

        public static bool GetMs2( Buse buse)
        {
            bool temp = false;
            lock (_ms2LockObj)
            {
                if (_ms2.Count > 0)
                {
                    if (buse == _ms2[0])
                    {
                        temp = true;
                        _ms2.RemoveAt(0);
                    }
                }
            }
            return temp;
        }

        public static void StartTownWatch()
        {
            while (!Stop)
            {
                if (_ms1.Count == 0)
                {
                    Thread.Sleep(1000);
                    continue;

                }
                lock (_ms1LockObj)
                {
                    int ID = 0;
                    for (int i = 1; i < _ms1.Count; i++)
                    {
                        if (_ms1[ID] > _ms1[i])
                            ID = i;
                    }
                    Thread.Sleep(_ms1[ID].GetNumberMinutesToStop() * 100);
                    for (int i = 0; i < _ms1.Count; i++)
                    {
                        _ms1[i] -= _ms1[ID];
                    }
                    
                    lock (_ms2LockObj)
                    {
                        _ms2.Add(_ms1[ID].GetBuse());
                    }
                    _ms1.RemoveAt(ID);
                }
            }
        }
    }
}
