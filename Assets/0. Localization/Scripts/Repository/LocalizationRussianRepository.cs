using System.Collections.Generic;
using System.IO;
using _0._Localization.Datas;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace _0._Localization.Scripts
{
    public class LocalizationRussianRepository : ILocalizationRepository
    {

        private const string PathRussianFile =
            "F:/Project/My New Project 2022/Unity Project/Donut Clicker/Clicker Donut/Assets/" +
            "0. Localization/Locales/LanguageRussian.json";
        
        

        public IDictionary<string,string> LoadWordsFromLanguage()
        {
            Debug.Log("[Russian] Loading words...");
            var objectData = new LocalizationData();
            var lines = File.ReadAllLines(PathRussianFile);
            var dictionary = new Dictionary<string, string>();
            
            foreach (var line in lines)
            {
                JsonUtility.FromJsonOverwrite(line, objectData);
                dictionary.Add(objectData.KeyWord, objectData.Translation);
            }
            
            Debug.Log("[Russian] Done.");
            return dictionary;
        }
    }
}