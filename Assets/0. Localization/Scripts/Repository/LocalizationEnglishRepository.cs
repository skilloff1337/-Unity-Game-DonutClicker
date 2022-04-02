using System.Collections.Generic;
using System.IO;
using _0._Localization.Datas;
using UnityEngine;

namespace _0._Localization.Scripts
{
    public class LocalizationEnglishRepository : ILocalizationRepository
    {
        private const string PathEnglishFile =
            "F:/Project/My New Project 2022/Unity Project/Donut Clicker/Clicker Donut/Assets/" +
            "0. Localization/Locales/LanguageEnglish.json";

        public IDictionary<string,string> LoadWordsFromLanguage()
        {
            Debug.Log("[English] Loading words...");
            var objectData = new LocalizationData();
            var lines = File.ReadAllLines(PathEnglishFile);
            var dictionary = new Dictionary<string, string>();
            
            foreach (var line in lines)
            {
                JsonUtility.FromJsonOverwrite(line, objectData);
                dictionary.Add(objectData.KeyWord, objectData.Translation);
            }

            Debug.Log("[English] Done.");
            return dictionary;
        }
    }
}