using System;
using MongoDB.Bson.Serialization.Attributes;

namespace _5._DataBase.Data
{
    public class StatisticsData
    {
        [BsonElement("clicks")] public double Clicks { get; set; }
        [BsonElement("clicksCurrentSession")] public double ClicksCurrentSession { get; set; }
        [BsonElement("upgradesData")] public double MaxClicksPerSession { get; set; }
        [BsonElement("bansForAntiClicker")] public double BansForAntiClicker { get; set; }
        [BsonElement("gameLogins")] public double GameLogins { get; set; }
        [BsonElement("viewsAds")] public double ViewsAds { get; set; }
        [BsonElement("currentPositionInLeadersBoard")] public double CurrentPositionInLeadersBoard { get; set; }
        [BsonElement("maxPositionInLeadersBoard")] public double MaxPositionInLeadersBoard { get; set; }
        [BsonElement("earnedExp")] public double EarnedExp { get; set; }

        [BsonElement("totalDamage")] public double TotalDamage { get; set; }
        [BsonElement("totalDamageLevel")] public double TotalDamageLevel { get; set; }
        [BsonElement("totalDamageUpgrade")] public double TotalDamageUpgrade { get; set; }
        [BsonElement("totalDamageDonut")] public double TotalDamageDonut { get; set; }

        [BsonElement("buyItemsInShop")] public double BuyItemsInShop { get; set; }
        [BsonElement("buyUpgradesInShop")] public double BuyUpgradesInShop { get; set; } 
        [BsonElement("buyDonateInShop")] public double BuyDonateInShop { get; set; }
        [BsonElement("buyFormForBaking")] public double BuyFormForBaking { get; set; }
        [BsonElement("buyBaker")] public double BuyBaker { get; set; }
        [BsonElement("buyFurnace")] public double BuyFurnace { get; set; }
        [BsonElement("buyConfectioneryStall")] public double BuyConfectioneryStall { get; set; }
        [BsonElement("buyConfectioneryShop")] public double BuyConfectioneryShop { get; set; }
        [BsonElement("buyBakery")] public double BuyBakery { get; set; }
        [BsonElement("buyFactory")] public double BuyFactory { get; set; }
        [BsonElement("buyMolecularKitchen")] public double BuyMolecularKitchen { get; set; }
        [BsonElement("buyLaboratory")] public double BuyLaboratory { get; set; }
        [BsonElement("buyStation")] public double BuyStation { get; set; }
        [BsonElement("buyDonutHole")] public double BuyDonutHole { get; set; }
        [BsonElement("buySource")] public double BuySource { get; set; }

        [BsonElement("earnedDonuts")] public double EarnedDonuts { get; set; } 
        [BsonElement("earnedWithClicks")] public double EarnedWithClicks { get; set; } 
        [BsonElement("earnedWithAds")] public double EarnedWithAds { get; set; }
        [BsonElement("earnedWithDps")] public double EarnedWithDps { get; set; } 
        [BsonElement("earnedWithDonate")] public double EarnedWithDonate { get; set; } 
        [BsonElement("earnedWithOffline")] public double EarnedWithOffline { get; set; } 
        [BsonElement("earnedDonate")] public double EarnedDonate { get; set; } 


        [BsonElement("spentDonuts")] public double SpentDonuts { get; set; } 
        [BsonElement("spentWithShop")] public double SpentWithShop { get; set; } 
        [BsonElement("spentWithUpgrade")] public double SpentWithUpgrade { get; set; } 
        [BsonElement("spentDonate")] public double SpentDonate { get; set; } 

        [BsonElement("firstLogin")] public DateTime FirstLogin { get; set; } = DateTime.Now; 
        [BsonElement("lastLogin")] public DateTime LastLogin { get; set; } = DateTime.Now; 
        [BsonElement("enterLogin")] public DateTime EnterLogin { get; set; } = DateTime.Now;
        [BsonElement("playedTime")] public DateTimeData PlayedTime { get; set; } = new(); 
        [BsonElement("longestSession")] public DateTimeData LongestSession { get; set; } = new(); 
    }
}