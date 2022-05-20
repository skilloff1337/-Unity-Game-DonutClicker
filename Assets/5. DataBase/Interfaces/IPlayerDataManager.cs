using _13._Achievements.Data;
using _14._Quests.Data;

namespace _5._DataBase.Interfaces
{
    public interface IPlayerDataManager
    {
        bool AddDonuts(double value);
        bool DelDonuts(double value);
        void SetDonutPerSeconds(double value);
        void SetX2DonutPerSeconds(bool value);
        void AddDonate(int value);
        bool DelDonate(int value);
        void AddStrengthClick(double value);
        void SetX2Click(bool value);
        void AddChanceCrit(int value);

        void AddValueCrit();
        void UpDonutLevel();

        void AddOfflineTime(int value);
        void AddOfflineProfitRatio(float value);

        void GiveOfflineDonuts();
        void GiveAchievements(int numAchievements, bool hasAward, AwardsList award, double awardValue, double points);
        void AddPointsAchievements(double value);

        void GiveQuests(int numQuests, QuestsAwards award, double awardValue);
        void SetCurrentQuests(int numQuests);

        void SetNickName(string newName);
        void SetSteamId(long id);
    }
}