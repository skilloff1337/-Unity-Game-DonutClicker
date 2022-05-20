using System.Collections.Generic;
using _0._Localization.Scripts.Interfaces;
using _11._Shop.Data;
using _4._Donuts.Scripts.Interfaces;
using _5._DataBase.Interfaces;
using _5._DataBase.Scripts;
using _8._Tooltips.Interfaces;
using _8._Tooltips.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _11._Shop.Scripts
{
    public class ShopSystem : MonoBehaviour, IShopSystem
    {
        public ShopData[] ShopItems
        {
            get => _items;
            set => _items = value;
        }

        [SerializeField] private ShopData[] _items;
        [SerializeField] private Sprite[] _iconItems;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _parent;

        private IPlayerData _playerData;
        private IPlayerDataManager _playerDataManager;
        private ILocalizationSystem _localizationSystem;
        private IDonutConvertSystem _donutConvertSystem;
        private ITooltipSystem _tooltipSystem;
        private IStatisticsDataManager _stats;

        private readonly ShopPriceCalculation _shopPrice = new ShopPriceCalculation();
        private readonly List<GameObject> _listPrefabs = new List<GameObject>();

        [Inject]
        private void Constructor(IPlayerData playerData, IPlayerDataManager playerDataManager,
            ILocalizationSystem localizationSystem, IDonutConvertSystem donutConvertSystem,
            ITooltipSystem tooltipSystem, IStatisticsDataManager stats)
        {
            _playerData = playerData;
            _playerDataManager = playerDataManager;
            _localizationSystem = localizationSystem;
            _donutConvertSystem = donutConvertSystem;
            _tooltipSystem = tooltipSystem;
            _stats = stats;
        }

        public void BuyShopItem(int idItem)
        {
            var price = _shopPrice.CostItem(_items[idItem].Level, _items[idItem].Price, _items[idItem].MultiplyPrice);
            if (!_playerDataManager.DelDonuts(price)) return;

            _items[idItem].Level++;
            _stats.AddBuyItemsInShop();
            _stats.AddBuyConcreteItemInShop(idItem);
            _stats.AddSpentWithShop(price);

            CalculatorDonutPerSeconds();
            UpdatePrefabs();
        }

        public void CalculatorDonutPerSeconds()
        {
            double donutPerSec = 0;

            foreach (var item in _items)
                donutPerSec += item.DonutPerSecond * item.Level * item.FactorDonutPerSec;

            donutPerSec *= _playerData.X2DonutPerSecond;
            _playerDataManager.SetDonutPerSeconds(donutPerSec);
        }

        public void CreateShopItemPrefab()
        {
            if (_listPrefabs.Count > 0)
            {
                UpdatePrefabs();
                return;
            }

            var numImage = 0;
            foreach (var item in _items)
            {
                var price = _shopPrice.CostItem(item.Level, item.Price, item.MultiplyPrice);
                var prefabObj = Instantiate(_prefab, _parent);

                var texts = prefabObj.GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = _localizationSystem.TranslateWord(item.NameTextID);
                texts[1].text = _donutConvertSystem.ConvertNumber(price);
                texts[2].text = $"[{_donutConvertSystem.ConvertNumber(item.Level)}]";

                var button = prefabObj.GetComponentInChildren<Button>();
                button.onClick.AddListener(delegate { BuyShopItem(item.IdItem); });
                button.interactable = !(price > _playerData.Donut);

                var textButton = button.GetComponentInChildren<TextMeshProUGUI>();
                textButton.text = _localizationSystem.TranslateWord(textButton.name);

                var image = prefabObj.GetComponentsInChildren<Image>();
                image[1].sprite = _iconItems[numImage];

                var tooltip = prefabObj.GetComponent<Tooltip>();
                tooltip.Fabric(_tooltipSystem, _localizationSystem);
                tooltip.BodyText = $"TOOLTIP_{item.NameTextID}";

                _listPrefabs.Add(prefabObj);
                numImage++;
            }
        }

        public void UpdatePrefabs()
        {
            for (var i = 0; i < _listPrefabs.Count; i++)
            {
                var price = _shopPrice.CostItem(_items[i].Level, _items[i].Price, _items[i].MultiplyPrice);

                var texts = _listPrefabs[i].GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = _localizationSystem.TranslateWord(_items[i].NameTextID);
                texts[1].text = _donutConvertSystem.ConvertNumber(price);
                texts[2].text = $"[{_donutConvertSystem.ConvertNumber(_items[i].Level)}]";


                var button = _listPrefabs[i].GetComponentInChildren<Button>();
                button.interactable = !(price > _playerData.Donut);

                var textButton = button.GetComponentInChildren<TextMeshProUGUI>();
                textButton.text = _localizationSystem.TranslateWord(textButton.name);
            }
        }
    }
}