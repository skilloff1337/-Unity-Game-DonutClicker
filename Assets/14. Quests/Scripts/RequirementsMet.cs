using System;
using _14._Quests.Data;
using _5._DataBase.Interfaces;

namespace _14._Quests.Scripts
{
    public class RequirementsMet
    {
        public bool Requirements(QuestsData quest, IPlayerData playerData)
        {
            var stats = playerData.StatisticsData;
            return quest.Required switch
            {
                QuestsRequired.Donut => playerData.Donut >= quest.RequiredValue,
                QuestsRequired.Level => playerData.LevelData.Level >= quest.RequiredValue,
                QuestsRequired.Exp => playerData.LevelData.Exp >= quest.RequiredValue,
                QuestsRequired.DonutPerSecond => playerData.DonutPerSecond >= quest.RequiredValue,
                QuestsRequired.BuyUpgrades => stats.BuyUpgradesInShop >= quest.RequiredValue,
                QuestsRequired.BuyItemsShop => stats.BuyItemsInShop >= quest.RequiredValue,
                QuestsRequired.SpentDonut => stats.SpentDonuts >= quest.RequiredValue,
                QuestsRequired.EarnedDonut => stats.EarnedDonuts >= quest.RequiredValue,
                QuestsRequired.TimePlayed => stats.PlayedTime.TotalSeconds() >= quest.RequiredValue,
                QuestsRequired.SpentDonate => stats.SpentDonate >= quest.RequiredValue,
                QuestsRequired.Clicks => stats.Clicks >= quest.RequiredValue,
                QuestsRequired.ClicksCurrentSession => stats.ClicksCurrentSession >= quest.RequiredValue,
                QuestsRequired.ShopFormForBaking => stats.BuyFormForBaking >= quest.RequiredValue,
                QuestsRequired.ShopBaker => stats.BuyBaker >= quest.RequiredValue,
                QuestsRequired.ShopFurnace => stats.BuyFurnace >= quest.RequiredValue,
                QuestsRequired.ShopConfectioneryStall => stats.BuyConfectioneryStall >= quest.RequiredValue,
                QuestsRequired.ShopConfectioneryShop => stats.BuyConfectioneryShop >= quest.RequiredValue,
                QuestsRequired.ShopBakery => stats.BuyBakery >= quest.RequiredValue,
                QuestsRequired.ShopFactory => stats.BuyFactory >= quest.RequiredValue,
                QuestsRequired.ShopMolecularKitchen => stats.BuyMolecularKitchen >= quest.RequiredValue,
                QuestsRequired.ShopLaboratory => stats.BuyLaboratory >= quest.RequiredValue,
                QuestsRequired.ShopStation => stats.BuyStation >= quest.RequiredValue,
                QuestsRequired.ShopDonutHole => stats.BuyDonutHole >= quest.RequiredValue,
                QuestsRequired.ShopSource => stats.BuySource >= quest.RequiredValue,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}