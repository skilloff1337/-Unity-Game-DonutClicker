namespace _5._DataBase.Interfaces
{
    public interface ILevelDataManager
    {
        void UpLevel();
        void AddExp(double value);
        void AddExpClick();
        void AddMultipleClick();
        void AddValueExpPerClick(double value);
        void SetX2ExpForClick(bool value);
    }
}