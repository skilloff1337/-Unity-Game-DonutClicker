using System;
using _13._Achievements.Data;
using _5._DataBase.Interfaces;

namespace _13._Achievements.Scripts
{
    public class AchievementsCalculator
    { 
        public float GetValue(RequiredList value, AchievementsData achievement, IPlayerData playerData)
        {
            return value switch
            {
                RequiredList.Donut => playerData.Donut >= achievement.RequiredValue
                    ? 100f
                    : (float) (playerData.Donut / achievement.RequiredValue * 100),
                RequiredList.Donate => playerData.Donate >= achievement.RequiredValue
                    ? 100f
                    : (float) (playerData.Donate / achievement.RequiredValue * 100),
                RequiredList.Level => playerData.LevelData.Level >= achievement.RequiredValue
                    ? 100f
                    : (float) (playerData.LevelData.Level / achievement.RequiredValue * 100),
                RequiredList.TotalExp => playerData.StatisticsData.EarnedExp >= achievement.RequiredValue
                    ? 100f
                    : (float) (playerData.StatisticsData.EarnedExp / achievement.RequiredValue * 100),
                RequiredList.TotalClick => playerData.StatisticsData.Clicks >= achievement.RequiredValue
                    ? 100f
                    : (float) (playerData.StatisticsData.Clicks / achievement.RequiredValue * 100),
                RequiredList.TotalSessionClick => playerData.StatisticsData.MaxClicksPerSession >=
                                                  achievement.RequiredValue
                    ? 100f
                    : (float) (playerData.StatisticsData.MaxClicksPerSession / achievement.RequiredValue * 100),
                RequiredList.TotalDPS => playerData.DonutPerSecond >= achievement.RequiredValue
                    ? 100f
                    : (float) (playerData.DonutPerSecond / achievement.RequiredValue * 100),
                RequiredList.TotalEarnedDonuts => playerData.StatisticsData.EarnedDonuts >= achievement.RequiredValue
                    ? 100f
                    : (float) (playerData.StatisticsData.EarnedDonuts / achievement.RequiredValue * 100),
                RequiredList.TotalSpentDonuts => playerData.StatisticsData.SpentDonuts >= achievement.RequiredValue
                    ? 100f
                    : (float) (playerData.StatisticsData.SpentDonuts / achievement.RequiredValue * 100),
                RequiredList.TotalBuyItems => playerData.StatisticsData.BuyItemsInShop >= achievement.RequiredValue
                    ? 100f
                    : (float) (playerData.StatisticsData.BuyItemsInShop / achievement.RequiredValue * 100),
                RequiredList.TotalBuyUpgrades => playerData.StatisticsData.BuyUpgradesInShop >=
                                                 achievement.RequiredValue
                    ? 100f
                    : (float) (playerData.StatisticsData.BuyUpgradesInShop / achievement.RequiredValue * 100),
                RequiredList.DonutLevel => playerData.DonutLevel >= achievement.RequiredValue
                    ? 100f
                    : (float) (playerData.DonutLevel / achievement.RequiredValue * 100),
                RequiredList.PlayingTime => playerData.StatisticsData.PlayedTime.TotalSeconds() >= achievement.RequiredValue
                    ? 100f
                    : (float) (playerData.StatisticsData.PlayedTime.TotalSeconds()/ achievement.RequiredValue * 100),
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }
    }
}