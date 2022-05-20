using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _11._Shop.Data;
using _11._Shop.Scripts;
using _5._DataBase.Data;
using _5._DataBase.Interfaces;
using MongoDB.Bson;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace _5._DataBase.Scripts.Save
{
    public class SavingSave : MonoBehaviour
    {
        private IPlayerData _playerData;
        private IShopSystem _shopSystem;
        private IStatisticsDataManager _statisticsDataManager;
        private IRepository _mongo;

        private const string PATH_SAVE_DATA =
            "F:/Project/My New Project 2022/Unity Project/Donut Clicker/Clicker Donut/Assets/" +
            "5. DataBase/DataPlayer/PlayerSave.json";

        private readonly byte[] _encKey = {112, 236, 235, 117, 27, 68, 44, 189};
        private readonly byte[] _encIv = {117, 102, 112, 80, 254, 43, 48, 197};

        private const int AUTO_SAVE_TIMER = 300;


        [Inject]
        private void Constructor(IPlayerData playerData, IShopSystem shopSystem,
            IStatisticsDataManager statisticsDataManager, IRepository mongo)
        {
            _playerData = playerData;
            _shopSystem = shopSystem;
            _statisticsDataManager = statisticsDataManager;
            _mongo = mongo;
        }

        private void OnApplicationQuit()
        {
            _statisticsDataManager.AllUpdateStatistics();
            SavingSaveFile();
        }

        private void Start()
        {
            StartCoroutine(AutoSave());
        }

        private void SaveMongoDataBase(PlayerData data)
        {
            if (!_mongo.Connection())
                return;

            if (_mongo.HasAccount(ObjectId.Parse(data.Id)))
                _mongo.Update(data);
            else
                _mongo.Create(data);
        }

        private void SavingSaveFile()
        {
            var saveData = SaveFile();
            SaveMongoDataBase(saveData);
            var save = JsonConvert.SerializeObject(saveData, Formatting.Indented);
            // Encryption.EncryptionSystem.EncryptTextToFile(save,PATH_SAVE_DATA, _encKey, _encIv);
            File.WriteAllText(PATH_SAVE_DATA, save);
        }

        private IEnumerator AutoSave()
        {
            while (true)
            {
                yield return new WaitForSeconds(AUTO_SAVE_TIMER);
                if (_mongo.Connection())
                    SaveMongoDataBase((PlayerData) _playerData);
                Debug.Log("AUTO SAVE!");
            }
        }

        private PlayerData SaveFile()
        {
            var save = new PlayerData
            {
                Id = _playerData.Id,
                NickName = _playerData.NickName,
                SteamID = _playerData.SteamID,

                Donut = _playerData.Donut,
                DonutPerSecond = _playerData.DonutPerSecond,
                Donate = _playerData.Donate,

                StrengthClick = _playerData.StrengthClick,
                X2DonutForClick = 1,

                DonutLevel = _playerData.DonutLevel,

                ChanceCrit = _playerData.ChanceCrit,
                ValueCrit = _playerData.ValueCrit,

                OfflineTime = _playerData.OfflineTime,
                ProfitRatio = _playerData.ProfitRatio,

                CompletedAchievements = _playerData.CompletedAchievements,
                PointsAchievements = _playerData.PointsAchievements,

                CompletedQuests = _playerData.CompletedQuests,
                CurrentQuest = _playerData.CurrentQuest,


                LevelData = LevelDataSave(),
                StatisticsData = StatisticsDataSave(),
                UpgradesData = UpgradesDataSave(),
                ShopData = ShopDataSave()
            };
            return save;
        }

        private List<ShopData> ShopDataSave() => _shopSystem.ShopItems.ToList();

        private UpgradesData UpgradesDataSave()
        {
            var upgrades = _playerData.UpgradesData;
            var data = new UpgradesData()
            {
                UpgradeClickLevel = upgrades.UpgradeClickLevel,
                UpgradeClickCost = upgrades.UpgradeClickCost,

                UpgradeDonutCost = upgrades.UpgradeDonutCost,

                UpgradeChanceCritCost = upgrades.UpgradeChanceCritCost,
                UpgradeValueCritCost = upgrades.UpgradeValueCritCost,
                UpgradeChanceCritLevel = upgrades.UpgradeChanceCritLevel,
                UpgradeValueCritLevel = upgrades.UpgradeValueCritLevel,

                UpgradeOfflineTimeCost = upgrades.UpgradeOfflineTimeCost,
                UpgradeProfitRatioCost = upgrades.UpgradeProfitRatioCost,
                UpgradeOfflineTimeLevel = upgrades.UpgradeOfflineTimeLevel,
                UpgradeProfitRatioLevel = upgrades.UpgradeProfitRatioLevel,
            };
            return data;
        }

        private LevelData LevelDataSave()
        {
            var level = _playerData.LevelData;
            var data = new LevelData
            {
                Level = level.Level,

                LevelMultipleClick = level.LevelMultipleClick,

                Exp = level.Exp,
                ExpForClick = level.ExpForClick,
                X2ExpForClick = 1,
                NeedExpForNextLevel = level.NeedExpForNextLevel
            };
            return data;
        }

        private StatisticsData StatisticsDataSave()
        {
            var stats = _playerData.StatisticsData;
            var data = new StatisticsData
            {
                Clicks = stats.Clicks,
                ClicksCurrentSession = 0,
                MaxClicksPerSession = stats.MaxClicksPerSession,
                BansForAntiClicker = stats.BansForAntiClicker,

                GameLogins = stats.GameLogins,
                ViewsAds = stats.ViewsAds,
                MaxPositionInLeadersBoard = stats.MaxPositionInLeadersBoard,
                CurrentPositionInLeadersBoard = stats.CurrentPositionInLeadersBoard,
                EarnedExp = stats.EarnedExp,

                TotalDamage = stats.TotalDamage,
                TotalDamageLevel = stats.TotalDamageLevel,
                TotalDamageUpgrade = stats.TotalDamageUpgrade,
                TotalDamageDonut = stats.TotalDamageDonut,

                BuyItemsInShop = stats.BuyItemsInShop,
                BuyUpgradesInShop = stats.BuyUpgradesInShop,
                BuyDonateInShop = stats.BuyDonateInShop,
                BuyFormForBaking = stats.BuyFormForBaking,
                BuyBaker = stats.BuyBaker,
                BuyFurnace = stats.BuyFurnace,
                BuyConfectioneryStall = stats.BuyConfectioneryStall,
                BuyConfectioneryShop = stats.BuyConfectioneryShop,
                BuyBakery = stats.BuyBakery,
                BuyFactory = stats.BuyFactory,
                BuyMolecularKitchen = stats.BuyMolecularKitchen,
                BuyLaboratory = stats.BuyLaboratory,
                BuyStation = stats.BuyStation,
                BuyDonutHole = stats.BuyDonutHole,
                BuySource = stats.BuySource,

                EarnedDonuts = stats.EarnedDonuts,
                EarnedWithClicks = stats.EarnedWithClicks,
                EarnedWithAds = stats.EarnedWithAds,
                EarnedWithDps = stats.EarnedWithDps,
                EarnedWithDonate = stats.EarnedWithDonate,
                EarnedWithOffline = stats.EarnedWithOffline,
                EarnedDonate = stats.EarnedDonate,

                SpentDonuts = stats.SpentDonuts,
                SpentWithShop = stats.SpentWithShop,
                SpentWithUpgrade = stats.SpentWithUpgrade,
                SpentDonate = stats.SpentDonate,

                EnterLogin = stats.EnterLogin,
                FirstLogin = stats.FirstLogin,
                LastLogin = DateTime.Now,
                PlayedTime = stats.PlayedTime,
                LongestSession = stats.LongestSession
            };
            return data;
        }
    }
}