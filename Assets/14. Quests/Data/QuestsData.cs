using UnityEngine;

namespace _14._Quests.Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateNewQuests", order = 1)]
    public class QuestsData : ScriptableObject
    {
        public int IdQuest;
        public string Name;
        public string HeaderTextID;
        public string BodyTextID;
        public QuestsRequired Required;
        public double RequiredValue;
        public QuestsAwards Awards;
        public double AwardsValue;
    }
}