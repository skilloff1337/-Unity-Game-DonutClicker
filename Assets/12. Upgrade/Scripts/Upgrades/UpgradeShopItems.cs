using _0._Localization.Scripts.Interfaces;
using _11._Shop.Scripts;
using _4._Donuts.Scripts.Interfaces;
using _5._DataBase.Interfaces;
using _5._DataBase.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _12._Upgrade.Scripts.Upgrades
{
    public class UpgradeShopItems : MonoBehaviour
    {
        [SerializeField] private int _idShopItem;

        private IShopSystem _shopSystem;
        private IPlayerDataManager _playerDataManager;
        private ILocalizationSystem _localizationSystem;
        private IDonutConvertSystem _donutConvertSystem;
        private IPlayerData _playerData;
        private IStatisticsDataManager _stats;

        private GameObject _prefab;

        private TextMeshProUGUI[] _texts;
        private TextMeshProUGUI HeaderText => _texts[0];
        private TextMeshProUGUI BodyText => _texts[1];
        private TextMeshProUGUI LevelText => _texts[2];
        private TextMeshProUGUI CostText => _texts[3];

        private Button _button;
        private TextMeshProUGUI _textButton;

        [Inject]
        private void Constructor(IShopSystem shopSystem, IPlayerDataManager playerDataManager,
            ILocalizationSystem localizationSystem, IDonutConvertSystem donutConvertSystem, IPlayerData playerData,
            IStatisticsDataManager stats)
        {
            _shopSystem = shopSystem;
            _playerDataManager = playerDataManager;
            _localizationSystem = localizationSystem;
            _donutConvertSystem = donutConvertSystem;
            _playerData = playerData;
            _stats = stats;
        }

        private void Awake()
        {
            UpdatePrefab();
        }

        public void UpdatePrefab()
        {
            if (_prefab == null)
                LoadComponentsForPrefab();
            
            HeaderText.text = _localizationSystem.TranslateWord(_shopSystem.ShopItems[_idShopItem].NameTextID);
            LevelText.text = "lvl:" + _shopSystem.ShopItems[_idShopItem].LevelUpgrade;
            BodyText.text = _localizationSystem.TranslateWord("UPGRADE_BODY_X2");
            CostText.text = _donutConvertSystem.ConvertNumber(_shopSystem.ShopItems[_idShopItem].CostUpgrade) +
                             "<sprite=0>";

            _button.interactable = !(_shopSystem.ShopItems[_idShopItem].CostUpgrade > _playerData.Donut);
            _textButton.text = _localizationSystem.TranslateWord(_textButton.name);
        }

        private void BuyUpgradeShopItem()
        {
            Debug.Log("BuyUpgradeShopItem");
            if (!_playerDataManager.DelDonuts(_shopSystem.ShopItems[_idShopItem].CostUpgrade))
                return;

            _shopSystem.ShopItems[_idShopItem].LevelUpgrade++;
            _shopSystem.ShopItems[_idShopItem].CostUpgrade *= 1_000;
            _shopSystem.ShopItems[_idShopItem].FactorDonutPerSec += 2;
            _shopSystem.CalculatorDonutPerSeconds();
            _stats.AddBuyUpgradesInShop();
            _stats.AddSpentWithUpgrade(_shopSystem.ShopItems[_idShopItem].CostUpgrade);
            UpdatePrefab();
        }

        private void LoadComponentsForPrefab()
        {
            _prefab = gameObject;

            _texts = _prefab.GetComponentsInChildren<TextMeshProUGUI>();

            _button = _prefab.GetComponentInChildren<Button>();
            _button.onClick.AddListener(BuyUpgradeShopItem);

            _textButton = _button.GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}