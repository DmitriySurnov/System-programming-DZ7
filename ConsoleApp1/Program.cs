using BusesInTown.Buses;
using BusesInTown.TownWatchs;
using System.Threading;
using System;
using ConsoleApp1.TownWatchUtility;

namespace ConsoleApp1
{
    class Program
    {
        public static TownWatchSingleProxy TownWatchProxy = new TownWatchSingleProxy();
        static void Main(string[] args)
        {
            Console.WriteLine("Симулятор загружен");
            new Thread(TownWatch.StartTownWatch).Start();
            TownWatchProxy.Start();
            new Thread(new Buse(10).star).Start();
            new Thread(new Buse(15).star).Start();
            new Thread(new Buse(18).star).Start();
            while (!TownWatch.Stop)
            {
                string temp = TownWatch.GetMessage();
                if (temp != "")
                    Console.WriteLine(temp);
                else
                    Thread.Sleep(1000);
            }
        }

    }
}
