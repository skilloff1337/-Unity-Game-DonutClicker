using System.Collections.Generic;
using _11._Shop.Data;
using _5._DataBase.Data;

namespace _5._DataBase.Interfaces
{
    public interface IPlayerData
    {
        public string Id { get; set; }
        public string NickName { get; set; }
        public long SteamID { get; set; }
        public double Donut { get; set; }
        public double DonutPerSecond { get; set; }
        public double X2DonutPerSecond { get; set; }
        
        public int Donate{ get; set; }

        public double StrengthClick{ get; set; }
        public int X2DonutForClick { get; set; }
        
        public int DonutLevel{ get; set; }
        public int MaxDonutLevel { get; }

        public int ChanceCrit { get; set; }
        public int ValueCrit { get; set; }

        public int OfflineTime { get; set; }
        public int MaxOfflineTime { get;}
        public float ProfitRatio { get; set; }
        public float MaxProfitRatio { get;}
        
        public List<int> CompletedAchievements { get; set; }
        public double PointsAchievements { get; set; }
        
        public List<int> CompletedQuests { get; set; }
        public int CurrentQuest { get; set; }

        public UpgradesData UpgradesData { get; set; }
        public LevelData LevelData { get; set; }
        public StatisticsData StatisticsData { get; set; }
        public List<ShopData> ShopData { get; set; }
    }
}