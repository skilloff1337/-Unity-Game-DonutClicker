using System;
using System.Collections.Generic;
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
        public LanguagesType currentLanguages;
        public Image[] ImageLanguage;
        
        private ILocalizationRepository _russianRepository;
        private ILocalizationRepository _englishRepository;

        private Dictionary<string, string> _russianWords = new Dictionary<string, string>();
        private Dictionary<string, string> _englishWords = new Dictionary<string, string>();


        [Inject]
        private void Constructor(
            [Inject(Id = "Russian")] ILocalizationRepository russianRepository,
            [Inject(Id = "English")] ILocalizationRepository englishRepository)
        {
            Debug.Log("Constructor LocalizationSystem");
            _russianRepository = russianRepository;
            _englishRepository = englishRepository;
            Debug.Log(_russianRepository);
            Debug.Log(_englishRepository);
        }

        private void Awake()
        {
            LoadingLanguages(); 
            SetAutomaticText();
        }

        private void LoadingLanguages()
        {
            Debug.Log("Loading Languages!");
            _russianWords = _russianRepository.LoadWordsFromLanguage();
            _englishWords = _englishRepository.LoadWordsFromLanguage();
            Debug.Log($"Loading end, english: {_englishWords.Count} russian: {_russianWords.Count}");
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
                             $"<color=red>{textID}</color>");
            return "Unknown!";
        }

        public void SwitchLanguage()
        {
            currentLanguages = currentLanguages switch
            {
                LanguagesType.Russian => LanguagesType.English,
                LanguagesType.English => LanguagesType.Russian,
                _ => throw new ArgumentOutOfRangeException()
            };
            SetAutomaticText();
        }

        public void SetAutomaticText()
        {
            var foundTexts = GetComponentsInChildren<TextMeshProUGUI>();
            Debug.Log("Find texts: " + foundTexts.Length);
            foreach (var text in foundTexts)
            {
                text.text = TranslateWord(text.name);
            }
        }
    }
}