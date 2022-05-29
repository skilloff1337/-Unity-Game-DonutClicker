using System;
using System.Collections.Generic;
using System.Diagnostics;
using _0._Localization.Lists;
using _0._Localization.Scripts.Interfaces;
using _1._Logs.Lists;
using _1._Logs.Scripts.Interfaces;
using _4._Donuts.Scripts;
using _4._Donuts.Scripts.Interfaces;
using _5._DataBase.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;
using Debug = UnityEngine.Debug;


namespace _0._Localization.Scripts
{
    public class LocalizationSystem : MonoBehaviour, ILocalizationSystem
    {
        private ILocalizationRepository _russianRepository;
        private ILocalizationRepository _englishRepository;
        private ISettingsData _settingsData;
        private IPlayerData _playerData;
        private ILogSystem _logSystem;
        private IOfflineBonus _offlineBonus;
        private IDonutConvertSystem _donutConvert;

        private Dictionary<string, string> _russianWords = new Dictionary<string, string>();
        private Dictionary<string, string> _englishWords = new Dictionary<string, string>();

        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly List<TextMeshProUGUI> _needTranslationTexts = new List<TextMeshProUGUI>();


        [Inject]
        private void Constructor(
            [Inject(Id = "Russian")] ILocalizationRepository russianRepository,
            [Inject(Id = "English")] ILocalizationRepository englishRepository, ISettingsData settingsData,
            IPlayerData playerData, ILogSystem logSystem, IOfflineBonus offlineBonus, IDonutConvertSystem donutConvert)
        {
            _russianRepository = russianRepository;
            _englishRepository = englishRepository;
            _settingsData = settingsData;
            _playerData = playerData;
            _logSystem = logSystem;
            _offlineBonus = offlineBonus;
            _donutConvert = donutConvert;
        }

        public void LoadingLanguages()
        {
            if (_russianWords.Count != 0 && _englishWords.Count != 0)
                return;
            
            Debug.Log("Loading Languages!");
            _stopwatch.Start();
            _russianWords = _russianRepository.LoadWordsFromLanguage();
            _englishWords = _englishRepository.LoadWordsFromLanguage();
            Debug.Log($"Loading end, english: {_englishWords.Count} " +
                      $"russian: {_russianWords.Count}, elapsed: {_stopwatch.Elapsed}");
        }

        public string TranslateWord(string textID)
        {
            switch (_settingsData.CurrentLanguages)
            {
                case LanguagesType.Russian:
                    if (_russianWords.TryGetValue(textID, out var russianResult))
                        return StringFormation(russianResult);
                    break;

                case LanguagesType.English:
                    if (_englishWords.TryGetValue(textID, out var englishResult))
                        return StringFormation(englishResult);
                    break;

                default:
                    _logSystem.AddLog(LogsType.Error,
                        $"[LocalizationSystem] TranslateWord, textId: {textID}, language: {_settingsData.CurrentLanguages}");
                    throw new ArgumentOutOfRangeException();
            }

            var textError = $"<color=red>[Error]</color> The System not found translate for: " +
                            $"<color=red>{textID}</color> Language: {_settingsData.CurrentLanguages}";
            _logSystem.AddLog(LogsType.Error, $"[LocalizationSystem] {textError}");
            return "Unknown!";
        }

        private string StringFormation(string value) => string.Format(value,
            _playerData.Donut,
            _playerData.Donate,
            _playerData.LevelData.Level,
            _playerData.LevelData.Exp,
            _playerData.LevelData.NeedExpForNextLevel,
            _playerData.DonutLevel,
            _playerData.StatisticsData.Clicks,
            _playerData.StatisticsData.ClicksCurrentSession,
            _playerData.StatisticsData.MaxClicksPerSession,
            _playerData.StatisticsData.BansForAntiClicker,
            _playerData.StatisticsData.GameLogins,
            _playerData.StatisticsData.ViewsAds,
            _playerData.StatisticsData.CurrentPositionInLeadersBoard,
            _playerData.StatisticsData.MaxPositionInLeadersBoard,
            _playerData.StatisticsData.EarnedExp,
            _playerData.StatisticsData.TotalDamage,
            _playerData.StatisticsData.TotalDamageLevel,
            _playerData.StatisticsData.TotalDamageUpgrade,
            _playerData.StatisticsData.TotalDamageDonut,
            _playerData.StatisticsData.BuyItemsInShop,
            _playerData.StatisticsData.BuyUpgradesInShop,
            _playerData.StatisticsData.BuyDonateInShop,
            _playerData.StatisticsData.EarnedDonuts,
            _playerData.StatisticsData.EarnedWithClicks,
            _playerData.StatisticsData.EarnedWithAds,
            _playerData.StatisticsData.EarnedWithDps,
            _playerData.StatisticsData.EarnedWithDonate,
            _playerData.StatisticsData.EarnedWithOffline,
            _playerData.StatisticsData.EarnedDonate,
            _playerData.StatisticsData.SpentDonuts,
            _playerData.StatisticsData.SpentWithShop,
            _playerData.StatisticsData.SpentWithUpgrade,
            _playerData.StatisticsData.SpentDonate,
            _playerData.StatisticsData.FirstLogin,
            _playerData.StatisticsData.LastLogin,
            _playerData.StatisticsData.PlayedTime.ShowTime(),
            _playerData.StatisticsData.LongestSession.ShowTime(),
            _offlineBonus.WasNotInTheGame(_playerData.StatisticsData.LastLogin),
            _playerData.OfflineTime,
            _playerData.ProfitRatio,
            _donutConvert.ConvertNumber(_offlineBonus.CountOfflineBonus(_playerData.StatisticsData.LastLogin,
                _playerData.OfflineTime, _playerData.ProfitRatio, _playerData.DonutPerSecond)));

        public void SetAutomaticText()
        {
            _stopwatch.Restart();
            if (_russianWords.Count == 0)
                LoadingLanguages();
            if (_needTranslationTexts.Count == 0)
                FindAndAddTextsInList();

            foreach (var text in _needTranslationTexts)
            {
                text.text = TranslateWord(text.name);
            }

            _stopwatch.Stop();
            Debug.Log($"Edit texts: {_needTranslationTexts.Count}, elapsed: {_stopwatch.Elapsed}");
        }

        private void FindAndAddTextsInList()
        {
            _stopwatch.Restart();

            var foundTexts = GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (var text in foundTexts)
            {
                if (!text.CompareTag("Ignore Translate"))
                    _needTranslationTexts.Add(text);
            }

            _stopwatch.Stop();
            Debug.Log($"Find Texts: {foundTexts.Length}, elapsed: {_stopwatch.Elapsed}");
        }
    }
}