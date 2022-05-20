using System;

namespace _4._Donuts.Scripts.Interfaces
{
    public interface IOfflineBonus
    {
        double CountOfflineBonus(DateTime lastEnter, int offlineTime, float profitRatio, double donutPerSec);
        string WasNotInTheGame(DateTime lastEnter);
    }
}