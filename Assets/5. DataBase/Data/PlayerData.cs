using System;
using System.Collections.Generic;
using System.Linq;
using _11._Shop.Data;
using _11._Shop.Scripts;
using _5._DataBase.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using UnityEngine;
using Random = System.Random;

namespace _5._DataBase.Data
{
    public class PlayerData : IPlayerData
    {
        [BsonId] [BsonRepresentation(BsonType.ObjectId)] 
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        
        [BsonElement("nickName")] public string NickName { get; set; }
        [BsonElement("steamID")] public long SteamID { get; set; } = 99999999999999999;
        [BsonElement("donut")] public double Donut { get; set; }
        [BsonElement("donutPerSecond")] public double DonutPerSecond { get; set; }
        [BsonElement("x2DonutPerSecond")] public double X2DonutPerSecond { get; set; } = 1;

        [BsonElement("donate")] public int Donate { get; set; }
        [BsonElement("strengthClick")] public double StrengthClick { get; set; } = 1;
        [BsonElement("x2DonutForClick")] public int X2DonutForClick { get; set; } = 1;

        [BsonElement("donutLevel")] public int DonutLevel { get; set; } = 1;
        [BsonIgnore] public int MaxDonutLevel => 8;

        [BsonElement("chanceCrit")] public int ChanceCrit { get; set; }
        [BsonElement("valueCrit")] public int ValueCrit { get; set; } = 2;

        [BsonElement("offlineTime")] public int OfflineTime { get; set; }
        [BsonIgnore] public int MaxOfflineTime => 86_400;
        [BsonElement("profitRatio")] public float ProfitRatio { get; set; }
        [BsonIgnore] public float MaxProfitRatio => 100f;

        [BsonElement("completedAchievements")] public List<int> CompletedAchievements { get; set; } = new();
        [BsonElement("pointsAchievements")] public double PointsAchievements { get; set; }

        [BsonElement("completedQuests")]public List<int> CompletedQuests { get; set; } = new();
        [BsonElement("currentQuest")] public int CurrentQuest { get; set; }

        [BsonElement("upgradesData")] public UpgradesData UpgradesData { get; set; } =new();
        [BsonElement("levelData")]public LevelData LevelData { get; set; } = new();
        [BsonElement("statisticsData")] public StatisticsData StatisticsData { get; set; } =  new();
        [BsonElement("shopData")] public List<ShopData> ShopData { get; set; } = new List<ShopData>();
    }
}