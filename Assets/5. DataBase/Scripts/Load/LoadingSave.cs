using System;
using System.IO;
using _1._Logs.Lists;
using _1._Logs.Scripts.Interfaces;
using _11._Shop.Scripts;
using _3._UI.Scripts.Interfaces;
using _5._DataBase.Data;
using _5._DataBase.Interfaces;
using MongoDB.Bson;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace _5._DataBase.Scripts.Load
{
    public class LoadingSave : MonoBehaviour
    {
        private IMediator _mediator;
        private IPlayerData _playerData;
        private ILogSystem _logSystem;
        private IPlayerDataManager _playerDataManager;
        private IStatisticsDataManager _statisticsDataManager;
        private IShopSystem _shopSystem;
        private IRepository _mongo;

        private string _pathSaveDataBuild;

        private const string PATH_SAVE_DATA_DEV =
            "F:/Project/My New Project 2022/Unity Project/Donut Clicker/Clicker Donut/Assets/" +
            "5. DataBase/DataPlayer/PlayerSave.json";

        private readonly byte[] _encKey = {112, 236, 235, 117, 27, 68, 44, 189};
        private readonly byte[] _encIv = {117, 102, 112, 80, 254, 43, 48, 197};

        [Inject]
        private void Constructor(IPlayerData playerData, IMediator mediator, ILogSystem logSystem,
            IPlayerDataManager playerDataManager, IShopSystem shopSystem, IStatisticsDataManager statisticsDataManager,
            IRepository mongo)
        {
            _playerData = playerData;
            _mediator = mediator;
            _logSystem = logSystem;
            _playerDataManager = playerDataManager;
            _shopSystem = shopSystem;
            _statisticsDataManager = statisticsDataManager;
            _mongo = mongo;
        }

        private void Awake()
        {
            _pathSaveDataBuild = Application.dataPath + "/DataPlayer/PlayerSave.json";
        }

        public void LoadSaveFile()
        {
            _statisticsDataManager.AddGameLogins();
            if (string.IsNullOrEmpty(_pathSaveDataBuild))
                _pathSaveDataBuild = Application.dataPath + "/DataPlayer/PlayerSave.json";
            
#if UNITY_EDITOR
            if (!File.Exists(PATH_SAVE_DATA_DEV))
            {
                UpdateTexts();
                return;
            }

            var lines = File.ReadAllText(PATH_SAVE_DATA_DEV);
            var saveData = JsonConvert.DeserializeObject<PlayerData>(lines);
            LoadAccountFromDataBaseOrFile(saveData);
            UpdateTexts();

#else
            if (!File.Exists(_pathSaveDataBuild))
            {
                UpdateTexts();
                return;
            }

            var lines = Encryption.EncryptionSystem.DecryptTextFromFile(_pathSaveDataBuild,_encKey, _encIv);
            var saveData = JsonConvert.DeserializeObject<PlayerData>(lines);
            LoadAccountFromDataBaseOrFile(saveData);
            UpdateTexts();
#endif
        }

        private void LoadAccountFromDataBaseOrFile(IPlayerData fileData)
        {
            var idMongo = ObjectId.Parse(fileData.Id);
            if (_mongo.HasAccount(idMongo) && _mongo.Connection())
            {
                var mongoData = _mongo.GetById(idMongo);

                SaveFile(mongoData.StatisticsData.LastLogin >= fileData.StatisticsData.LastLogin
                    ? mongoData
                    : fileData);
            }
            else
                SaveFile(fileData);
        }

        private void SaveFile(IPlayerData player)
        {
            try
            {
                SavePlayerData(player);
                SaveLevelsData(player);
                SaveStatisticsData(player);
                SaveUpgradesData(player);
                SaveShopDatas(player);
            }
            catch (Exception e)
            {
                const string textError = "The system was unable to load the save";
                _logSystem.AddLog(LogsType.Error, $"[LoadingSave] {textError} {e}");
            }

            _playerDataManager.GiveOfflineDonuts();
        }

        private void SaveShopDatas(IPlayerData player)
        {
            _shopSystem.ShopItems = player.ShopData.ToArray();
        }

        private void SaveUpgradesData(IPlayerData player)
        {
            _playerData.UpgradesData.UpgradeClickLevel = player.UpgradesData.UpgradeClickLevel;
            _playerData.UpgradesData.UpgradeClickCost = player.UpgradesData.UpgradeClickCost;

            _playerData.UpgradesData.UpgradeDonutCost = player.UpgradesData.UpgradeDonutCost;

            _playerData.UpgradesData.UpgradeChanceCritCost = player.UpgradesData.UpgradeChanceCritCost;
            _playerData.UpgradesData.UpgradeValueCritCost = player.UpgradesData.UpgradeValueCritCost;
            _playerData.UpgradesData.UpgradeChanceCritLevel = player.UpgradesData.UpgradeChanceCritLevel;
            _playerData.UpgradesData.UpgradeValueCritLevel = player.UpgradesData.UpgradeValueCritLevel;

            _playerData.UpgradesData.UpgradeOfflineTimeCost = player.UpgradesData.UpgradeOfflineTimeCost;
            _playerData.UpgradesData.UpgradeProfitRatioCost = player.UpgradesData.UpgradeProfitRatioCost;
            _playerData.UpgradesData.UpgradeOfflineTimeLevel = player.UpgradesData.UpgradeOfflineTimeLevel;
            _playerData.UpgradesData.UpgradeProfitRatioLevel = player.UpgradesData.UpgradeProfitRatioLevel;
        }

        private void SavePlayerData(IPlayerData player)
        {
            _playerData.Id = player.Id;
            _playerData.NickName = player.NickName;
            _playerData.SteamID = player.SteamID;

            _playerData.Donut = player.Donut;
            _playerData.DonutPerSecond = player.DonutPerSecond;
            _playerData.Donate = player.Donate;

            _playerData.StrengthClick = player.StrengthClick;

            _playerData.X2DonutForClick = player.X2DonutForClick;

            _playerData.DonutLevel = player.DonutLevel;

            _playerData.ChanceCrit = player.ChanceCrit;
            _playerData.ValueCrit = player.ValueCrit;

            _playerData.OfflineTime = player.OfflineTime;
            _playerData.ProfitRatio = player.ProfitRatio;

            _playerData.CompletedAchievements = player.CompletedAchievements;
            _playerData.PointsAchievements = player.PointsAchievements;

            _playerData.CompletedQuests = player.CompletedQuests;
            _playerData.CurrentQuest = player.CurrentQuest;
        }

        private void SaveStatisticsData(IPlayerData player)
        {
            _statisticsDataManager.AllUpdateStatistics();

            _playerData.StatisticsData.Clicks = player.StatisticsData.Clicks;
            _playerData.StatisticsData.MaxClicksPerSession = player.StatisticsData.MaxClicksPerSession;
            _playerData.StatisticsData.BansForAntiClicker = player.StatisticsData.BansForAntiClicker;

            _playerData.StatisticsData.GameLogins = player.StatisticsData.GameLogins;
            _playerData.StatisticsData.ViewsAds = player.StatisticsData.ViewsAds;
            _playerData.StatisticsData.MaxPositionInLeadersBoard = player.StatisticsData.MaxPositionInLeadersBoard;
            _playerData.StatisticsData.CurrentPositionInLeadersBoard =
                player.StatisticsData.CurrentPositionInLeadersBoard;
            _playerData.StatisticsData.EarnedExp = player.StatisticsData.EarnedExp;

            _playerData.StatisticsData.TotalDamage = player.StatisticsData.TotalDamage;
            _playerData.StatisticsData.TotalDamageLevel = player.StatisticsData.TotalDamageLevel;
            _playerData.StatisticsData.TotalDamageUpgrade = player.StatisticsData.TotalDamageUpgrade;
            _playerData.StatisticsData.TotalDamageDonut = player.StatisticsData.TotalDamageDonut;

            _playerData.StatisticsData.BuyItemsInShop = player.StatisticsData.BuyItemsInShop;
            _playerData.StatisticsData.BuyUpgradesInShop = player.StatisticsData.BuyUpgradesInShop;
            _playerData.StatisticsData.BuyDonateInShop = player.StatisticsData.BuyDonateInShop;
            _playerData.StatisticsData.BuyFormForBaking = player.StatisticsData.BuyFormForBaking;
            _playerData.StatisticsData.BuyBaker = player.StatisticsData.BuyBaker;
            _playerData.StatisticsData.BuyFurnace = player.StatisticsData.BuyFurnace;
            _playerData.StatisticsData.BuyConfectioneryStall = player.StatisticsData.BuyConfectioneryStall;
            _playerData.StatisticsData.BuyConfectioneryShop = player.StatisticsData.BuyConfectioneryShop;
            _playerData.StatisticsData.BuyBakery = player.StatisticsData.BuyBakery;
            _playerData.StatisticsData.BuyFactory = player.StatisticsData.BuyFactory;
            _playerData.StatisticsData.BuyMolecularKitchen = player.StatisticsData.BuyMolecularKitchen;
            _playerData.StatisticsData.BuyLaboratory = player.StatisticsData.BuyLaboratory;
            _playerData.StatisticsData.BuyStation = player.StatisticsData.BuyStation;
            _playerData.StatisticsData.BuyDonutHole = player.StatisticsData.BuyDonutHole;
            _playerData.StatisticsData.BuySource = player.StatisticsData.BuySource;

            _playerData.StatisticsData.EarnedDonuts = player.StatisticsData.EarnedDonuts;
            _playerData.StatisticsData.EarnedWithClicks = player.StatisticsData.EarnedWithClicks;
            _playerData.StatisticsData.EarnedWithAds = player.StatisticsData.EarnedWithAds;
            _playerData.StatisticsData.EarnedWithDps = player.StatisticsData.EarnedWithDps;
            _playerData.StatisticsData.EarnedWithDonate = player.StatisticsData.EarnedWithDonate;
            _playerData.StatisticsData.EarnedWithOffline = player.StatisticsData.EarnedWithOffline;
            _playerData.StatisticsData.EarnedDonate = player.StatisticsData.EarnedDonate;

            _playerData.StatisticsData.SpentDonuts = player.StatisticsData.SpentDonuts;
            _playerData.StatisticsData.SpentWithShop = player.StatisticsData.SpentWithShop;
            _playerData.StatisticsData.SpentWithUpgrade = player.StatisticsData.SpentWithUpgrade;
            _playerData.StatisticsData.SpentDonate = player.StatisticsData.SpentDonate;

            _playerData.StatisticsData.EnterLogin = DateTime.Now;
            _playerData.StatisticsData.FirstLogin = player.StatisticsData.FirstLogin;
            _playerData.StatisticsData.LastLogin = player.StatisticsData.LastLogin;
            _playerData.StatisticsData.PlayedTime = player.StatisticsData.PlayedTime;
            _playerData.StatisticsData.LongestSession = player.StatisticsData.LongestSession;
        }

        private void SaveLevelsData(IPlayerData player)
        {
            _playerData.LevelData.Level = player.LevelData.Level;

            _playerData.LevelData.LevelMultipleClick = player.LevelData.LevelMultipleClick;

            _playerData.LevelData.Exp = player.LevelData.Exp;
            _playerData.LevelData.ExpForClick = player.LevelData.ExpForClick;
            _playerData.LevelData.X2ExpForClick = 1;
            _playerData.LevelData.NeedExpForNextLevel = player.LevelData.NeedExpForNextLevel;
        }

        private void UpdateTexts()
        {
            _mediator.UpdateDonutScore();
            _mediator.UpdateDonateScore();
            _mediator.UpdateDonutPerClick();
            _mediator.UpdateDonutPerSeconds();
            _mediator.UpdateLevelPlayer();
        }
    }
}