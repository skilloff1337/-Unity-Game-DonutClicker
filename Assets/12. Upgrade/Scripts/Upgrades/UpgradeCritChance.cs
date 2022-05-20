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
    public class UpgradeCritChance : MonoBehaviour
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
            if (_headerText == null)
                LoadComponentsForPrefab();
            LoadComponentsForPrefab();
            UpdatePrefab();
        }

        public void UpdatePrefab()
        {
            if (_headerText == null)
                LoadComponentsForPrefab();
            
            _headerText.text = _localizationSystem.TranslateWord("UPGRADE_HEADER_CHANCE_CRIT");
            _levelText.text = "lvl:" + _playerData.UpgradesData.UpgradeChanceCritLevel;
            _bodyText.text = _localizationSystem.TranslateWord("UPGRADE_BODY_CHANCE_CRIT");
            _costText.text = _donutConvertSystem.ConvertNumber(_playerData.UpgradesData.UpgradeChanceCritCost) +
                             "<sprite=0>";

            _button.interactable = !(_playerData.UpgradesData.UpgradeChanceCritCost > _playerData.Donut);
            _textButton.text = _localizationSystem.TranslateWord(_textButton.name);
        }

        private void BuyCritValueItem()
        {
            if (!_playerDataManager.DelDonuts(_playerData.UpgradesData.UpgradeChanceCritCost))
                return;

            _playerDataManager.AddChanceCrit(1);
            _upgradesDataManager.AddChanceCritLevel();
            _upgradesDataManager.SetChanceCritCost(_playerData.UpgradesData.UpgradeChanceCritCost * 100);
            _stats.AddBuyUpgradesInShop();
            _stats.AddSpentWithUpgrade(_playerData.UpgradesData.UpgradeChanceCritCost);
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
            _button.onClick.AddListener(BuyCritValueItem);

            _textButton = _button.GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}