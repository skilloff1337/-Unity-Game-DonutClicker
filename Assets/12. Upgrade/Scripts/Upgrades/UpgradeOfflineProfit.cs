using _0._Localization.Scripts.Interfaces;
using _4._Donuts.Scripts.Interfaces;
using _5._DataBase.Interfaces;
using _5._DataBase.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _12._Upgrade.Scripts.Upgrades
{
    public class UpgradeOfflineProfit : MonoBehaviour
    {
        private IPlayerDataManager _playerDataManager;
        private ILocalizationSystem _localizationSystem;
        private IDonutConvertSystem _donutConvertSystem;
        private IPlayerData _playerData;
        private IUpgradesDataManager _upgradesDataManager;
        private IStatisticsDataManager _stats;

        private GameObject _prefab;
        private TextMeshProUGUI _headerText;
        private TextMeshProUGUI _bodyText;
        private TextMeshProUGUI _levelText;
        private TextMeshProUGUI _costText;
        private Button _button;
        private TextMeshProUGUI _textButton;


        [Inject]
        private void Constructor(IPlayerDataManager playerDataManager, ILocalizationSystem localizationSystem, 
            IDonutConvertSystem donutConvertSystem, IPlayerData playerData, IUpgradesDataManager upgradesDataManager, 
            IStatisticsDataManager stats)
        {
            _playerDataManager = playerDataManager;
            _localizationSystem = localizationSystem;
            _donutConvertSystem = donutConvertSystem;
            _playerData = playerData;
            _upgradesDataManager = upgradesDataManager;
            _stats = stats;
        }

        private void Awake()
        {
            LoadComponentsForPrefab();
            UpdatePrefab();
        }

        public void UpdatePrefab()
        {
            if (_headerText == null)
                LoadComponentsForPrefab();
            
            _headerText.text = _localizationSystem.TranslateWord("UPGRADE_HEADER_OFFLINE_PROFIT_RATIO");
            _levelText.text = "lvl:" + _playerData.UpgradesData.UpgradeProfitRatioLevel;
            _bodyText.text = _localizationSystem.TranslateWord("UPGRADE_BODY_OFFLINE_PROFIT_RATIO");
            _costText.text = _donutConvertSystem.ConvertNumber(_playerData.UpgradesData.UpgradeProfitRatioCost) +
                             "<sprite=0>";

            _button.interactable = !(_playerData.UpgradesData.UpgradeProfitRatioCost > _playerData.Donut);
            _textButton.text = _localizationSystem.TranslateWord(_textButton.name);
        }

        private void BuyOfflineTimeItem()
        {
            if (!_playerDataManager.DelDonuts(_playerData.UpgradesData.UpgradeProfitRatioCost))
                return;

            _playerDataManager.AddOfflineProfitRatio(5);
            _upgradesDataManager.AddUpgradeOfflineProfitRatioLevel();
            _upgradesDataManager.SetUpgradeOfflineProfitRatioCost(_playerData.UpgradesData.UpgradeProfitRatioCost * 100);
            _stats.AddBuyUpgradesInShop();
            _stats.AddSpentWithUpgrade(_playerData.UpgradesData.UpgradeProfitRatioCost);
            UpdatePrefab();
        }

        private void LoadComponentsForPrefab()
        {
            _prefab = gameObject;

            var texts = _prefab.GetComponentsInChildren<TextMeshProUGUI>();
            _headerText = texts[0];
            _levelText = texts[1];
            _bodyText = texts[2];
            _costText = texts[3];

            _button = _prefab.GetComponentInChildren<Button>();
            _button.onClick.AddListener(BuyOfflineTimeItem);

            _textButton = _button.GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}