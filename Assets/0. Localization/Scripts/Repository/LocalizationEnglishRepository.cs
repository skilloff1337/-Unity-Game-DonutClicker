using System.Collections.Generic;
using System.IO;
using _0._Localization.Data;
using _0._Localization.Scripts.Interfaces;
using UnityEngine;

namespace _0._Localization.Scripts.Repository
{
    public class LocalizationEnglishRepository : ILocalizationRepository
    {
        private const string PATH_ENGLISH_FILE_DEV =
            "F:/Project/My New Project 2022/Unity Project/Donut Clicker/Clicker Donut/Assets/" +
            "0. Localization/Locales/LanguageEnglish.json";

        private readonly string _pathEnglishFile = Path.Combine(Application.dataPath,"DataPlayer", "LanguageEnglish.json");
        public Dictionary<string,string> LoadWordsFromLanguage()
        {
            var objectData = new LocalizationData();
#if UNITY_EDITOR
            var lines = File.ReadAllLines(PATH_ENGLISH_FILE_DEV);
#else
            var lines = File.ReadAllLines(_pathEnglishFile);
#endif
            var dictionary = new Dictionary<string, string>();
            
            foreach (var line in lines)
            {
                JsonUtility.FromJsonOverwrite(line, objectData);
                dictionary.Add(objectData.KeyWord, objectData.Translation);
            }
            return dictionary;
        }
    }
}