using System.Collections;
using System.Diagnostics;
using _0._Localization.Scripts.Interfaces;
using _10._Statistics.Scripts;
using _11._Shop.Scripts;
using _12._Upgrade.Scripts;
using _13._Achievements.Scripts;
using _14._Quests.Scripts;
using _4._Donuts.Scripts;
using _5._DataBase.Interfaces;
using _5._DataBase.Scripts;
using _5._DataBase.Scripts.Load;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Debug = UnityEngine.Debug;
using Random = System.Random;

namespace _2._Loadings.Scripts
{
    public class LoadingSystem : MonoBehaviour
    {
        [SerializeField] private UpgradesSystem _upgradesSystem;
        [SerializeField] private QuestsSystem _questsSystem;
        [SerializeField] private StatisticsSystem _statisticsSystem;
        [SerializeField] private AchievementsSystem _achievementsSystem;
        [SerializeField] private LoadingSettings _loadingSettings;
        [SerializeField] private LoadingSave _loadingSave;
        [SerializeField] private ClickerSystem _clickerSystem;

        [SerializeField] [Range(1, 50)] private float _speedLoading;
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private GameObject _offlineBonusObject;
        [SerializeField] private TextMeshProUGUI _textSlider;
        [SerializeField] private TextMeshProUGUI _textLoading;

        private IShopSystem _shopSystem;
        private ILocalizationSystem _local;
        private IPlayerData _playerData;

        private Slider _slider;

        private readonly Stopwatch _stopwatch = new();
        private readonly Random _random = new();


        [Inject]
        private void Constructor(IShopSystem shopSystem, ILocalizationSystem local, IPlayerData playerData)
        {
            _shopSystem = shopSystem;
            _local = local;
            _playerData = playerData;
        }

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
            LoadingGame();
        }

        private void LoadingGame()
        {
            _stopwatch.Restart();
            Debug.Log($"<color=red>LOADING GAME START:</color>");

            LoadingSettingsData();
            LoadingSaveData();
            LoadingLocalization();
            LoadingShop();
            LoadingUpgrades();
            LoadingQuests();
            LoadingStatistics();
            LoadingAchievements();
            LoadingSpriteDonut();
            LoadingTranslation();

            _stopwatch.Stop();
            Debug.Log($"<color=red>LOADING GAME END, ELAPSED: {_stopwatch.Elapsed}:</color>");
        }

        private void Start()
        {
            StartCoroutine(Loading());
            StartCoroutine(SetText());
        }

        private IEnumerator SetText()
        {
            var num = 1;
            var mainText = _textLoading.text;
            while (_slider.value < 100f)
            {
                switch (num)
                {
                    case > 0 and < 4:
                        _textLoading.text += ".";
                        break;
                    case 4:
                        _textLoading.text = mainText;
                        num = 0;
                        break;
                }

                num++;
                yield return new WaitForSeconds(1);
            }
        }

        private IEnumerator Loading()
        {
            while (_slider.value < 100f)
            {
                _slider.value += _speedLoading * Time.deltaTime;
                _textSlider.text = $"{_slider.value:0.0}%";
                yield return new WaitForEndOfFrame();
            }

            _gameObject.SetActive(false);

            if (_playerData.OfflineTime > 0 && _playerData.ProfitRatio > 0)
                _offlineBonusObject.SetActive(true);

            if (string.IsNullOrEmpty(_playerData.NickName))
                _playerData.NickName = "Player №" + _random.Next(0, 100_000);
        }

        private void LoadingSettingsData() => _loadingSettings.LoadSettingsFile();
        private void LoadingSaveData() => _loadingSave.LoadSaveFile();
        private void LoadingLocalization() => _local.LoadingLanguages();
        private void LoadingTranslation() => _local.SetAutomaticText();
        private void LoadingShop() => _shopSystem.CreateShopItemPrefab();
        private void LoadingUpgrades() => _upgradesSystem.UpdatePrefabs();
        private void LoadingQuests() => _questsSystem.CreatePrefabsButton();
        private void LoadingStatistics() => _statisticsSystem.CreatePrefabStatistics();
        private void LoadingAchievements() => _achievementsSystem.CreatePrefabs();
        private void LoadingSpriteDonut() => _clickerSystem.LoadSpriteDonut();
    }
}