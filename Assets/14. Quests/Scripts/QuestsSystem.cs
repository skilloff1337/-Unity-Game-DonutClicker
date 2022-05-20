using System.Collections.Generic;
using _0._Localization.Scripts.Interfaces;
using _14._Quests.Data;
using _5._DataBase.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _14._Quests.Scripts
{
    public class QuestsSystem : MonoBehaviour
    {
        [SerializeField] private QuestsData[] _quests;
        [SerializeField] private GameObject _prefabButton;
        [SerializeField] private GameObject _prefabPanel;
        [SerializeField] private Transform _parentButton;
        [SerializeField] private Transform _parentPanel;

        private IPlayerData _playerData;
        private ILocalizationSystem _local;
        private IPlayerDataManager _playerManager;
        
        private readonly RequirementsMet _requirements = new RequirementsMet();
        private readonly List<GameObject> _listPrefabsButton = new List<GameObject>();
        private readonly List<GameObject> _listPrefabsPanel = new List<GameObject>();

        [Inject]
        private void Constructor(IPlayerData playerData, ILocalizationSystem local, IPlayerDataManager dataManager)
        {
            _playerData = playerData;
            _local = local;
            _playerManager = dataManager;
        }

        private GameObject CreatePanelPrefab(int index)
        {
            var quest = _quests[index];

            var prefab = Instantiate(_prefabPanel, _parentPanel);
            prefab.name = "Panel_" + quest.name;

            var texts = prefab.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = $"{_local.TranslateWord(quest.HeaderTextID)}[{index+1}]";
            texts[1].text = $"{_local.TranslateWord(quest.BodyTextID)}";
            texts[2].text = $"<color=blue>{_local.TranslateWord("REQUIRED")}:</color>\n{quest.Required}({quest.RequiredValue})";
            texts[3].text = $"<color=green>{_local.TranslateWord("AWARD")}:</color>\n{quest.Awards}({quest.AwardsValue})";
            


            var buttons = prefab.GetComponentsInChildren<Button>();
            buttons[0].onClick.AddListener(delegate { HidePanelQuests(prefab); });
            
            buttons[1].interactable = false;
            buttons[1].onClick.AddListener(delegate { GetAward(quest); });
            
            var buttonText = buttons[1].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = _local.TranslateWord("GET");

            if (_playerData.CompletedQuests.Contains(index))
                buttonText.text = _local.TranslateWord("QUESTS_AWARD_RECEIVED");

            if (_requirements.Requirements(quest, _playerData) && _playerData.CompletedQuests.Contains(index) == false)
                buttons[1].interactable = true;
            
            prefab.SetActive(false);
            _listPrefabsPanel.Add(prefab);

            return prefab;
        }

        public void CreatePrefabsButton()
        {
            for (var i = 0; i < _quests.Length; i++)
            {
                var prefab = Instantiate(_prefabButton, _parentButton);
                prefab.name = "Button_" + i;

                prefab.GetComponentInChildren<TextMeshProUGUI>().text = $"{i+1}";

                var button = prefab.GetComponent<Button>();
                var image = button.GetComponent<Image>();
                if(_playerData.CompletedQuests.Contains(i))
                    image.color = Color.green;
                else if (_playerData.CurrentQuest == i)
                    image.color = Color.cyan;
                else
                {
                    image.color = Color.grey;
                    button.interactable = false;
                }

                var prefabPanel = CreatePanelPrefab(i);
                button.onClick.AddListener(delegate { ShowPanelQuests(prefabPanel); });
                _listPrefabsButton.Add(prefab);
            }
        }

        public void UpdatePrefabs()
        {
            UpdateButtonPrefabs();
            UpdatePanelPrefabs();
        }

        private void UpdatePanelPrefabs()
        {
            for (var i = 0; i < _listPrefabsPanel.Count; i++)
            {
                var prefab = _listPrefabsPanel[i];
                var buttons = prefab.GetComponentsInChildren<Button>();
                buttons[1].interactable = false;
                buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = _local.TranslateWord("GET");

                if (_playerData.CompletedQuests.Contains(i))
                    buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = _local.TranslateWord("QUESTS_AWARD_RECEIVED");

                if (_requirements.Requirements(_quests[i], _playerData) && _playerData.CompletedQuests.Contains(i) == false)
                    buttons[1].interactable = true;
            }
        }

        private void UpdateButtonPrefabs()
        {
            for (var i = 0; i < _listPrefabsButton.Count; i++)
            {
                var prefab = _listPrefabsButton[i];
                var button = prefab.GetComponent<Button>();
                button.interactable = true;

                var image = button.GetComponent<Image>();
                if (_playerData.CompletedQuests.Contains(i))
                    image.color = Color.green;
                else if (_playerData.CurrentQuest == i)
                    image.color = Color.cyan;
                else
                {
                    image.color = Color.grey;
                    button.interactable = false;
                }
            }
        }

        private void GetAward(QuestsData quest)
        {
            _playerManager.GiveQuests(quest.IdQuest, quest.Awards, quest.AwardsValue);
            UpdatePrefabs();
        }

        private void ShowPanelQuests(GameObject panel)
        {
            panel.SetActive(true);
        }

        private void HidePanelQuests(GameObject panel)
        {
            panel.SetActive(false);
        }
    }
}