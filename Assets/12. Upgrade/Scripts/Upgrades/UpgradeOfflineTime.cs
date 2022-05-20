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
    public class UpgradeOfflineTime : MonoBehaviour
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

            _headerText.text = _localizationSystem.TranslateWord("UPGRADE_HEADER_OFFLINE_TIME");
            _levelText.text = "lvl:" + _playerData.UpgradesData.UpgradeOfflineTimeLevel;
            _bodyText.text = _localizationSystem.TranslateWord("UPGRADE_BODY_OFFLINE_TIME");
            _costText.text = _donutConvertSystem.ConvertNumber(_playerData.UpgradesData.UpgradeOfflineTimeCost) +
                             "<sprite=0>";

            _button.interactable = !(_playerData.UpgradesData.UpgradeOfflineTimeCost > _playerData.Donut);
            _textButton.text = _localizationSystem.TranslateWord(_textButton.name);
        }

        private void BuyOfflineItem()
        {
            if (!_playerDataManager.DelDonuts(_playerData.UpgradesData.UpgradeOfflineTimeCost))
                return;

            _playerDataManager.AddOfflineTime(600);
            _upgradesDataManager.AddUpgradeOfflineTimeLevel();
            _upgradesDataManager.SetUpgradeOfflineTimeCost(_playerData.UpgradesData.UpgradeOfflineTimeCost * 50);
            _stats.AddBuyUpgradesInShop();
            _stats.AddSpentWithUpgrade(_playerData.UpgradesData.UpgradeOfflineTimeCost);
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
            _button.onClick.AddListener(BuyOfflineItem);

            _textButton = _button.GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}