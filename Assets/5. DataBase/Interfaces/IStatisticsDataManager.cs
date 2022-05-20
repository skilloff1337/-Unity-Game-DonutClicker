namespace _5._DataBase.Interfaces
{
    public interface IStatisticsDataManager
    {
        void UpdateStatistics();
        void UpdateStatisticsOnExit();
        void AllUpdateStatistics();
        void AddClicks();
        void AddClicksCurrentSession();
        void SetMaxClicksPerSession();
        void AddBansForAntiClicker();
        void AddGameLogins();
        void AddViewsAds();
        void SetCurrentPositionInLeadersBoard(double value);
        void AddEarnedExp(double value);

        void SetTotalDamage();
        void SetTotalDamageLevel();
        void SetTotalDamageUpgrade();
        void SetTotalDamageDonut();

        void AddBuyItemsInShop();
        void AddBuyUpgradesInShop();
        void AddBuyDonateInShop();
        void AddBuyConcreteItemInShop(int value);

        void AddEarnedDonuts(double value);
        void AddEarnedWithClicks(double value);
        void AddEarnedWithAds(double value);
        void AddEarnedWithDps(double value);
        void AddEarnedWithDonate(double value);
        void AddEarnedWithOffline(double value);
        void AddEarnedDonate(double value);

        void AddSpentDonuts(double value);
        void AddSpentWithShop(double value);
        void AddSpentWithUpgrade(double value);
        void AddSpentDonate(double value);
        
        void AddPlayedTime();
        void SetLongestSession();
    }
}