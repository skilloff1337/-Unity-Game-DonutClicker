using _7._Level.Interfaces;

namespace _7._Level.Scripts
{
    public class SystemExp : ISystemExp
    {
        public double NeedExpForNextLevel(int currentLevel) =>
            currentLevel is >= 1 and <= 10 ? currentLevel * 100 :
            currentLevel is >= 11 and <= 20 ? currentLevel * 200 :
            currentLevel is >= 21 and <= 30 ? currentLevel * 300 :
            currentLevel is >= 31 and <= 40 ? currentLevel * 400 :
            currentLevel is >= 41 and <= 50 ? currentLevel * 500 :
            currentLevel is >= 51 and <= 60 ? currentLevel * 1000 :
            currentLevel is >= 61 and <= 70 ? currentLevel * 1200 :
            currentLevel is >= 71 and <= 80 ? currentLevel * 1400 :
            currentLevel is >= 81 and <= 90 ? currentLevel * 1600 :
            currentLevel is >= 91 and <= 98 ? currentLevel * 1800 :
            currentLevel == 99 ? 1_000_000 : 
            currentLevel == 100 ? 0 : double.MaxValue;
    }
}