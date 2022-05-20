using UnityEngine;

namespace _13._Achievements.Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateNewAchievements", order = 1)]
    public class AchievementsData : ScriptableObject
    {
        public int IdAchievemets;
        public QualityList Quality;
        public string NameAchievements;
        public string HeaderTextID;
        public string BodyTextID;
        public int CompletingPoints;
        public bool HasAward;
        public AwardsList Award;
        public double AwardValue;
        public RequiredList RequiredForAchievements;
        public double RequiredValue;
    }
}