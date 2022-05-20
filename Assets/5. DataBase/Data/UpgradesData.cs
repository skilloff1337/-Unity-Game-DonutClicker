using MongoDB.Bson.Serialization.Attributes;

namespace _5._DataBase.Data
{
    public class UpgradesData
    {
        
        [BsonElement("upgradeClickLevel")] public double UpgradeClickLevel { get; set; }
        [BsonElement("upgradeClickCost")] public double UpgradeClickCost { get; set; } = 1_000;
        
        [BsonElement("upgradeDonutCost")] public double UpgradeDonutCost { get; set; } = 1_000_000; 
        
        [BsonElement("upgradeChanceCritCost")] public double UpgradeChanceCritCost { get; set; } = 100_000; 
        [BsonElement("upgradeValueCritCost ")] public double UpgradeValueCritCost { get; set; } = 1_000_000_000; 
        [BsonElement("upgradeChanceCritLevel")] public double UpgradeChanceCritLevel { get; set; }
        [BsonElement("upgradeValueCritLevel ")] public double UpgradeValueCritLevel { get; set; }
        
        [BsonElement("upgradeOfflineTimeCost ")] public double UpgradeOfflineTimeCost { get; set; } = 100_000;
        [BsonElement("upgradeProfitRatioCost")] public double UpgradeProfitRatioCost { get; set; } = 100_000;
        [BsonElement("upgradeOfflineTimeLevel")] public double UpgradeOfflineTimeLevel { get; set; }
        [BsonElement("upgradeProfitRatioLevel")] public double UpgradeProfitRatioLevel { get; set; }
    }
}