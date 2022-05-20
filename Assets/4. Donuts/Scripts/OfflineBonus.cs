using System;
using _4._Donuts.Scripts.Interfaces;

namespace _4._Donuts.Scripts
{
    public class OfflineBonus : IOfflineBonus
    {
        public double CountOfflineBonus(DateTime lastEnter, int offlineTime, float profitRatio, double donutPerSec)
        {
            var donuts = 0d;
            var time = DateTime.Now - lastEnter;

            if (time.TotalSeconds >= offlineTime)
                donuts = offlineTime * donutPerSec * (profitRatio / 100);
               
            else
                donuts = offlineTime * time.TotalSeconds * (profitRatio / 100);
            
            return Math.Round(donuts);
        }

        public string WasNotInTheGame(DateTime lastEnter)
        {
            var time =  DateTime.Now - lastEnter;
            return $"D: {time.Days},H: {time.Hours},M: {time.Minutes},S: {time.Seconds}";
        }
    }
}