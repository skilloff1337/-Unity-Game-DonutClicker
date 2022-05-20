using MongoDB.Bson.Serialization.Attributes;

namespace _5._DataBase.Data
{
    public class LevelData
    {
        [BsonIgnore] public int MaxLevel => 100;
        [BsonElement("level")]public int Level { get; set; } = 1;
        [BsonElement("levelMultipleClick")] public double LevelMultipleClick { get; set; }
        [BsonElement("exp")] public double Exp { get; set; }
        [BsonElement("expForClick")] public double ExpForClick { get; set; } = 1;
        [BsonElement("x2ExpForClick")] public int X2ExpForClick { get; set; } = 1;
        [BsonElement("needExpForNextLevel")] public double NeedExpForNextLevel { get; set; } = 100;
    }
}