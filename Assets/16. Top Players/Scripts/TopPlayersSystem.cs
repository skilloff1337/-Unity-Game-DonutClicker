using System;
using System.Collections;
using System.Collections.Generic;
using _4._Donuts.Scripts.Interfaces;
using _5._DataBase.Data;
using _5._DataBase.Interfaces;
using _5._DataBase.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _16._Top_Players.Scripts
{
    public class TopPlayersSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private GameObject _panelOffline;
        [SerializeField] private Transform _parentDonut;
        [SerializeField] private Transform _parentClicks;
        [SerializeField] private Transform _parentDps;
        [SerializeField] private TextMeshProUGUI _textTimer;
        
        private IDonutConvertSystem _convert;
        private IRepository _mongo;
        private IPlayerData _playerData;
        private IStatisticsDataManager _stats;

        private int _updateTime;
        private const int UPDATE_EVERY = 300;
        
        private readonly List<GameObject> _listPrefabsDonut = new();
        private readonly List<GameObject> _listPrefabsClicks = new();
        private readonly List<GameObject> _listPrefabsDps = new();

        private readonly List<PlayerData> _topPlayerDonut = new();
        private readonly List<PlayerData> _topPlayerClicks = new();
        private readonly List<PlayerData> _topPlayerDps = new();


        [Inject]
        private void Constructor(IDonutConvertSystem convertSystem, IRepository mongo,
            IPlayerData playerData, IStatisticsDataManager stats)
        {
            _convert = convertSystem;
            _mongo = mongo;
            _playerData = playerData;
            _stats = stats;
        }

        private void Start()
        {
            CreatePrefabs();
            StartCoroutine(TimerUpdater());
        }

        private IEnumerator TimerUpdater()
        {
            while (true)
            {
                if (_updateTime <= 0)
                {
                    if (!_mongo.Connection())
                        ShowOfflinePanel(true);
                    else
                    {
                        if (_panelOffline.activeSelf)
                            ShowOfflinePanel(false);

                        UpdateTopPlayers();
                        UpdatePrefabs();
                    }

                    _updateTime = UPDATE_EVERY;
                    _textTimer.text = $"{_updateTime}";
                }

                yield return new WaitForSeconds(1);
                _updateTime--;
                _textTimer.text = $"{_updateTime}";
            }
        }

        private void ShowOfflinePanel(bool action)
        {
            _panelOffline.SetActive(action);
        }

        private void UpdateTopPlayers()
        {
            var donut = _mongo.GetTopPlayerDonuts();
            if (_topPlayerDonut.Count > 0)
                _topPlayerDonut.Clear();
            _topPlayerDonut.AddRange(donut);

            var clicks = _mongo.GetTopPlayerClicks();
            if (_topPlayerClicks.Count > 0)
                _topPlayerClicks.Clear();
            _topPlayerClicks.AddRange(clicks);

            var dps = _mongo.GetTopPlayerDps();
            if (_topPlayerDps.Count > 0)
                _topPlayerDps.Clear();
            _topPlayerDps.AddRange(dps);
        }

        private void CreatePrefabs()
        {
            for (var i = 0; i < 3; i++)
            {
                var parent = i switch
                {
                    0 => _parentDonut,
                    1 => _parentClicks,
                    _ => _parentDps
                };
                var namePrefab = i switch
                {
                    0 => "Donut Player №",
                    1 => "Clicks Player №",
                    _ => "DPS Player №"
                };
                for (var j = 0; j < 11; j++)
                {
                    var prefab = Instantiate(_prefab, parent);
                    prefab.name = namePrefab + (j + 1);

                    var background = prefab.GetComponent<Image>();
                    background.color = j % 2 == 0 ? Color.grey : Color.cyan;
                    if (j == 10)
                        background.color = Color.green;

                    switch (i)
                    {
                        case 0:
                            _listPrefabsDonut.Add(prefab);
                            break;
                        case 1:
                            _listPrefabsClicks.Add(prefab);
                            break;
                        case 2:
                            _listPrefabsDps.Add(prefab);
                            break;
                    }
                }
            }
        }

        private void UpdatePrefabs()
        {
            var numDonut = _topPlayerDonut.FindIndex(x => x.Id == _playerData.Id) + 1;
            var numClicks = _topPlayerClicks.FindIndex(x => x.Id == _playerData.Id) + 1;
            var numDps = _topPlayerDps.FindIndex(x => x.Id == _playerData.Id) + 1;

            var maxPos = Math.Max(numDonut, Math.Max(numClicks, numDps));
            _stats.SetCurrentPositionInLeadersBoard(maxPos);

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 11; j++)
                {
                    if (j >= _topPlayerDonut.Count)
                        break;

                    var prefab = i == 0 ? _listPrefabsDonut[j] : i == 1 ? _listPrefabsClicks[j] : _listPrefabsDps[j];
                    var text = prefab.GetComponentInChildren<TextMeshProUGUI>();
                    switch (i)
                    {
                        case 0:
                            text.text = $"[{j + 1}] {_topPlayerDonut[j].NickName} : " +
                                        $"{_convert.ConvertNumber(_topPlayerDonut[j].Donut)}<sprite=0>";
                            if (j == 10)
                                text.text = $"[{numDonut}] {_playerData.NickName} : " +
                                            $"{_convert.ConvertNumber(_playerData.Donut)}<sprite=0>";
                            break;
                        case 1:
                            text.text = $"[{j + 1}] {_topPlayerClicks[j].NickName} : " +
                                        $"{_convert.ConvertNumber(_topPlayerDonut[j].StatisticsData.Clicks)}<sprite=2>";
                            if (j == 10)
                                text.text = $"[{numClicks}] {_playerData.NickName} : " +
                                            $"{_convert.ConvertNumber(_playerData.StatisticsData.Clicks)}<sprite=2>";
                            break;
                        case 2:
                            text.text = $"[{j + 1}] {_topPlayerDps[j].NickName} : " +
                                        $"{_convert.ConvertNumber(_topPlayerDps[j].DonutPerSecond)}<sprite=3>";
                            if (j == 10)
                                text.text = $"[{numDps}] {_playerData.NickName} : " +
                                            $"{_convert.ConvertNumber(_playerData.DonutPerSecond)}<sprite=3>";
                            break;
                        default:
                            text.text = text.text;
                            break;
                    }
                }
            }
        }
    }
}