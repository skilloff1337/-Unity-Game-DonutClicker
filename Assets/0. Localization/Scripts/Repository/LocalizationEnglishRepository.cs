using System.Collections.Generic;
using System.IO;
using _0._Localization.Datas;
using UnityEngine;

namespace _0._Localization.Scripts
{
    public class LocalizationEnglishRepository : ILocalizationRepository
    {
        private const string PATH_ENGLISH_FILE =
            "F:/Project/My New Project 2022/Unity Project/Donut Clicker/Clicker Donut/Assets/" +
            "0. Localization/Locales/LanguageEnglish.json";

        public Dictionary<string,string> LoadWordsFromLanguage()
        {
            var objectData = new LocalizationData();
            var lines = File.ReadAllLines(PATH_ENGLISH_FILE);
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