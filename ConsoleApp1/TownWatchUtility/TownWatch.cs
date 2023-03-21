using BusesInTown.Messages;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BusesInTown.TownWatchs
{
    internal class TownWatch
    {
        private readonly List<Message> _listMessage;
        private readonly object _listMessageLockObj;
        private bool _stop;
        public TownWatch() {
            _listMessage = new List<Message>();
            _listMessageLockObj = new object();
            _stop = true;
        }
        public void Add(Action actionToInvoke, int numberMinutesToStop)
        {
            lock (_listMessageLockObj)
            {
                _listMessage.Add(new Message(actionToInvoke, numberMinutesToStop));
            }
        }

        public void Start()
        {
            if (!_stop)
                return;
            _stop = false;
            new Thread(Countdown).Start();
        }
        public void Stop()
        {
            _stop = true;
            lock (_listMessageLockObj)
                _listMessage.Clear();
        }

        private void Countdown()
        {
            while (!_stop)
            {
                if (_listMessage.Count == 0)
                {
                    Thread.Sleep(1000);
                    continue;
                }
                lock (_listMessageLockObj)
                {
                    int ID = 0;
                    for (int i = 1; i < _listMessage.Count; i++)
                    {
                        if (_listMessage[ID] > _listMessage[i])
                            ID = i;
                    }
                    Thread.Sleep(_listMessage[ID].GetNumberMinutesToStop() * 1000);
                    for (int i = 0; i < _listMessage.Count; i++)
                    {
                        _listMessage[i] -= _listMessage[ID];
                    }
                    _listMessage[ID].Action();
                    _listMessage.RemoveAt(ID);
                }
            }
        }

    }
}
