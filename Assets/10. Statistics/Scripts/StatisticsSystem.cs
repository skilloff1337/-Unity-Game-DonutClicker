using System.Collections.Generic;
using _0._Localization.Scripts.Interfaces;
using _5._DataBase.Interfaces;
using _5._DataBase.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _10._Statistics.Scripts
{
    public class StatisticsSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _parent;
        
        private ILocalizationSystem _localizationSystem;
        private IStatisticsDataManager _statisticsDataManager;
        
        private readonly List<GameObject> _listPrefabs = new List<GameObject>();
        private readonly string[] _listTextIDStatistics = 
        {
            "STATISTICS_CLICKS", "STATISTICS_CLICKSCURRENTSESSION", "STATISTICS_MAXCLICKSPERSESSION",
            "STATISTICS_BANSFORANTICLICKER", "STATISTICS_GAMELOGINS", "STATISTICS_VIEWSADS",
            "STATISTICS_CURRENTPOSITIONINLEADERSBOARD", "STATISTICS_MAXPOSITIONINLEADERSBOARD",
            "STATISTICS_EARNEDEXP", "STATISTICS_TOTALDAMAGE", "STATISTICS_TOTALDAMAGELEVEL",
            "STATISTICS_TOTALDAMAGEUPGRADE", "STATISTICS_TOTALDAMAGEDONUT", "STATISTICS_BUYITEMSINSHOP",
            "STATISTICS_BUYUPGRADESINSHOP", "STATISTICS_BUYDONATEINSHOP", "STATISTICS_EARNEDDONUTS",
            "STATISTICS_EARNEDWITHCLICKS", "STATISTICS_EARNEDWITHADS", "STATISTICS_EARNEDWITHDPS",
            "STATISTICS_EARNEDWITHDONATE", "STATISTICS_EARNEDWITHOFFLINE", "STATISTICS_EARNEDDONATE",
            "STATISTICS_SPENTDONUTS", "STATISTICS_SPENTWITHSHOP", "STATISTICS_SPENTWITHUPGRADE",
            "STATISTICS_SPENTDONATE", "STATISTICS_FIRSTLOGIN", "STATISTICS_LASTLOGIN", "STATISTICS_PLAYEDTIME",
            "STATISTICS_LONGESTSESSION"
        };

        [Inject]
        private void Constructor(ILocalizationSystem localizationSystem,
            IStatisticsDataManager statisticsDataManager)
        {
            _localizationSystem = localizationSystem;
            _statisticsDataManager = statisticsDataManager;
        }

        public void CreatePrefabStatistics()
        {
            if (_listPrefabs.Count > 0)
            {
                return;
            }

            var i = 0;
            foreach (var textID in _listTextIDStatistics)
            {
                var prefabObj = Instantiate(_prefab, _parent);
                var text = prefabObj.GetComponentInChildren<TextMeshProUGUI>();
                var image = prefabObj.GetComponent<Image>();
                text.text = _localizationSystem.TranslateWord(textID);
                text.name = textID;
                prefabObj.name = textID;
                image.color = i % 2 == 0 ? Color.grey : Color.white;
                _listPrefabs.Add(prefabObj);
                i++;
            }
        }

        public void UpdateTextsPrefab()
        {
            _statisticsDataManager.UpdateStatistics();
            foreach (var prefabObj in _listPrefabs)
            {
                var text = prefabObj.GetComponentInChildren<TextMeshProUGUI>();
                text.text = _localizationSystem.TranslateWord(text.name);
            }
        }
    }
}