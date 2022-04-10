using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using _0._Localization.Interfaces;
using _0._Localization.Lists;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Debug = UnityEngine.Debug;


namespace _0._Localization.Scripts
{
    public class LocalizationSystem : MonoBehaviour, ILocalizationSystem
    {
        [SerializeField] private LanguagesType currentLanguages;
        public LanguagesType CurrentLanguage => currentLanguages;
        

        public Sprite[] ImageLanguage;

        [SerializeField] private Image _imageLanguageSelect;

        private ILocalizationRepository _russianRepository;
        private ILocalizationRepository _englishRepository;

        private Dictionary<string, string> _russianWords = new Dictionary<string, string>();
        private Dictionary<string, string> _englishWords = new Dictionary<string, string>();

        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly List<TextMeshProUGUI> _needTranslationTexts = new List<TextMeshProUGUI>();


        [Inject]
        private void Constructor(
            [Inject(Id = "Russian")] ILocalizationRepository russianRepository,
            [Inject(Id = "English")] ILocalizationRepository englishRepository)
        {
            _russianRepository = russianRepository;
            _englishRepository = englishRepository;
        }

        private void Awake()
        {
            LoadingLanguages();
            SetAutomaticText();
            UpdateCurrentFlagLanguage();
        }

        private void UpdateCurrentFlagLanguage()
        {
            _imageLanguageSelect.sprite = currentLanguages switch
            {
                LanguagesType.Russian => ImageLanguage[0],
                LanguagesType.English => ImageLanguage[1],
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void LoadingLanguages()
        {
            Debug.Log("Loading Languages!");
            _stopwatch.Start();
            _russianWords = _russianRepository.LoadWordsFromLanguage();
            _englishWords = _englishRepository.LoadWordsFromLanguage();
            Debug.Log($"Loading end, english: {_englishWords.Count} " +
                      $"russian: {_russianWords.Count}, elapsed: {_stopwatch.Elapsed}");
        }

        public string TranslateWord(string textID)
        {
            switch (currentLanguages)
            {
                case LanguagesType.Russian:
                    if (_russianWords.TryGetValue(textID, out var russianResult))
                        return russianResult;
                    break;

                case LanguagesType.English:
                    if (_englishWords.TryGetValue(textID, out var englishResult))
                        return englishResult;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            Debug.LogWarning($"<color=red>[Error]</color> The System not found translate for: " +
                             $"<color=red>{textID}</color> {textID}");
            return "Unknown!";
        }

        public void SwitchLanguage()
        {
            switch (currentLanguages)
            {
                case LanguagesType.Russian:
                    currentLanguages = LanguagesType.English;
                    break;
                case LanguagesType.English:
                    currentLanguages = LanguagesType.Russian;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            UpdateCurrentFlagLanguage();
            SetAutomaticText();
        }

        public void SetAutomaticText()
        {
            _stopwatch.Restart();
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