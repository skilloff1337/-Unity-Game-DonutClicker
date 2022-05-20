using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UnityEngine;

namespace _11._Shop.Data
{
    
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateNewItemShop", order = 1)]
    public class ShopData : ScriptableObject
    {
        [BsonElement("itemType")][BsonRepresentation(BsonType.String)]public ItemType Item;
        [BsonElement("idItem")]public int IdItem;
        [BsonElement("nameTextID")]public string NameTextID;
        [BsonElement("description")]public string Description;
        [BsonElement("level")]public double Level;
        [BsonElement("price")]public double Price;
        [BsonElement("donutPerSecond")]public double DonutPerSecond;
        [BsonElement("factorDonutPerSec")]public double FactorDonutPerSec = 1;
        [BsonElement("levelUpgrade")]public double LevelUpgrade = 1;
        [BsonElement("costUpgrade")]public double CostUpgrade = 100_000;
        [BsonElement("multiplyPrice")]public float MultiplyPrice;
    }
}