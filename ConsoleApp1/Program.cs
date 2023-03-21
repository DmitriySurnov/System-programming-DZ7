using BusesInTown.Buss;
using BusesInTown.TownWatchs;
using System.Threading;
using System;
using ConsoleApp1.TownWatchUtility;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusesInTown.Routes;

namespace ConsoleApp1
{
    class Program
    {
        public static TownWatchSingleProxy TownWatchProxy = new TownWatchSingleProxy();
        public static TownWatch townWatchs = new TownWatch();
        private static readonly List<Bus> _busList = new List<Bus>();
        static void Main(string[] args)
        {
            Console.WriteLine("Симулятор загружен");
            townWatchs.Start();
            TownWatchProxy.Start();
            List<BaseRoute> routes = new List<BaseRoute>(){
                RouteFactory.CreateRoute(10),
                RouteFactory.CreateRoute(15),
                RouteFactory.CreateRoute(18),
            };
            _busList.Add(new Bus(10, routes[0], Write));
            _busList.Add(new Bus(15, routes[1], Write));
            _busList.Add(new Bus(18, routes[2], Write));
            for (int i = 0; i < _busList.Count; i++)
                new Thread(_busList[i].Start).Start();
            Exit();
        }

        private static void Write(string message)
        {
            Console.WriteLine(message);
        }

        private static async void Exit()
        {
            await Task.Run(() =>
            {
                ConsoleKeyInfo keyInfo;
                do
                {
                    keyInfo = Console.ReadKey();
                } while (keyInfo.Key != ConsoleKey.Enter);
            });
            TownWatchProxy.Stop();
            townWatchs.Stop();
            for (int i = 0; i < _busList.Count; i++)
                _busList[i].Stop();
            Environment.Exit(0);
        }

    }
}
