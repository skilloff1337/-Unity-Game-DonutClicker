using System.Collections.Generic;
using _0._Localization.Scripts.Interfaces;
using _13._Achievements.Data;
using _5._DataBase.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _13._Achievements.Scripts
{
    public class AchievementsSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private AchievementsData[] _achievements;
        [SerializeField] private Sprite[] _backGroundSprite;

        private IPlayerData _playerData;
        private ILocalizationSystem _local;
        private IPlayerDataManager _playerManager;
        
        private readonly List<GameObject> _prefabsList = new List<GameObject>();
        private readonly AchievementsCalculator _achievementsCalculator = new AchievementsCalculator();


        [Inject]
        private void Constructor(IPlayerData playerData, ILocalizationSystem localizationSystem,
            IPlayerDataManager playerDataManager)
        {
            _playerData = playerData;
            _local = localizationSystem;
            _playerManager = playerDataManager;
        }

        public void CreatePrefabs()
        {
            CheckCompletedAchievements();
            if (_prefabsList.Count > 0)
            {
                return;
            }

            foreach (var ach in _achievements)
            {
                var prefab = Instantiate(_prefab, _parent);
                prefab.name = ach.NameAchievements;

                var slider = prefab.GetComponentInChildren<Slider>();
                slider.value = _achievementsCalculator.GetValue(ach.RequiredForAchievements, ach, _playerData);

                var textSlider = slider.GetComponentInChildren<TextMeshProUGUI>();
                textSlider.text = slider.value <= 0.9 ? "0%" : $"{slider.value:#,#}%";

                var texts = prefab.GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = _local.TranslateWord(ach.HeaderTextID);
                texts[1].text = _local.TranslateWord(ach.BodyTextID);
                texts[2].text = $"{ach.Quality}({ach.CompletingPoints})";
                if (ach.HasAward)
                    texts[3].text = $"{_local.TranslateWord("AWARD")}: {ach.Award}({ach.AwardValue})";

                var background = prefab.GetComponent<Image>();
                background.sprite = _backGroundSprite[NumSpriteBackGround(ach.Quality)];

                if (_playerData.CompletedAchievements.Contains(ach.IdAchievemets))
                {
                    prefab.GetComponentInChildren<Animation>().Play("Achievements_Prefab");
                    textSlider.text = $"100%";
                    slider.value = 100f;
                }


                _prefabsList.Add(prefab);
            }
        }

        public void UpdatePrefabs()
        {
            CheckCompletedAchievements();
            for (var i = 0; i < _prefabsList.Count; i++)
            {
                var ach = _achievements[i];

                var texts = _prefabsList[i].GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].text = _local.TranslateWord(ach.HeaderTextID);
                texts[1].text = _local.TranslateWord(ach.BodyTextID);
                
                var slider = _prefabsList[i].GetComponentInChildren<Slider>();
                slider.value = _achievementsCalculator.GetValue(ach.RequiredForAchievements, ach,
                    _playerData);
                var textSlider = slider.GetComponentInChildren<TextMeshProUGUI>();
                textSlider.text = slider.value <= 0.9 ? "0%" : $"{slider.value:#,#}%";

                if (!_playerData.CompletedAchievements.Contains(ach.IdAchievemets)) continue;

                _prefabsList[i].GetComponentInChildren<Animation>().Play();
                textSlider.text = $"100%";
                slider.value = 100f;
            }
        }

        private void CheckCompletedAchievements()
        {
            for (var i = 0; i < _achievements.Length; i++)
            {
                var ach = _achievements[i];

                if (_playerData.CompletedAchievements.Contains(i))
                    continue;

                if (_achievementsCalculator.GetValue(ach.RequiredForAchievements, ach, _playerData) >= 100f)
                    _playerManager.GiveAchievements(i, ach.HasAward, ach.Award, ach.AwardValue, ach.CompletingPoints);
            }
        }

        private int NumSpriteBackGround(QualityList quality) =>
            quality switch
            {
                QualityList.Common => 0,
                QualityList.Rare => 1,
                QualityList.Legendary => 2,
                _ => 3
            };
    }
}