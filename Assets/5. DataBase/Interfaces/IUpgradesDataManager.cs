namespace _5._DataBase.Interfaces
{
    public interface IUpgradesDataManager
    {
        void SetUpgradeDonutCost(double value);
        void SetChanceCritCost(double value);
        void SetValueCritCost(double value);
        void AddChanceCritLevel();
        void AddCritValueLevel();
        void AddUpgradeClickLevel();
        void SetUpgradeClickCost(double value);
        void SetUpgradeOfflineTimeCost(double value);
        void SetUpgradeOfflineProfitRatioCost(double value);
        void AddUpgradeOfflineTimeLevel();
        void AddUpgradeOfflineProfitRatioLevel();
    }
}