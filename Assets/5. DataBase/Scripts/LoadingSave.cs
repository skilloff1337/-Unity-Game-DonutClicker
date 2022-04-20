using System;
using System.IO;
using _3._UI.Scripts;
using _5._DataBase.Data;
using _5._DataBase.Interfaces;
using Newtonsoft.Json;
//using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace _5._DataBase.Scripts
{
    public class LoadingSave : MonoBehaviour
    {

        private IMediator _mediator;
        private IPlayerData _playerData;

        private const string PATH_SAVE_DATA =
            "F:/Project/My New Project 2022/Unity Project/Donut Clicker/Clicker Donut/Assets/" +
            "5. DataBase/DataPlayer/PlayerSave.json";

        [Inject]
        private void Constructor(IPlayerData playerData, IMediator mediator)
        {
            _playerData = playerData;
            _mediator = mediator;
        }

        private void Awake()
        {
            LoadSaveFile();
        }
        
        private void LoadSaveFile()
        {
            if(!File.Exists(PATH_SAVE_DATA)) return;
            var lines = File.ReadAllText(PATH_SAVE_DATA);
            var save = JsonConvert.DeserializeObject<PlayerData>(lines);
            SaveFile(save);
            UpdateTexts();
        }
        private void SaveFile(PlayerData player)
        {
            _playerData.Donut = player.Donut;
            _playerData.Donate = player.Donate;
            _playerData.StrengthClick = player.StrengthClick;
            _playerData.FactorClick = player.FactorClick;
            _playerData.DonutLevel = player.DonutLevel;
            _playerData.Level = player.Level;
            _playerData.Exp = player.Exp;
            _playerData.ChanceCrit = player.ChanceCrit;
            _playerData.StatisticsData.Clicks = player.StatisticsData.Clicks;
            _playerData.StatisticsData.EnterGame = player.StatisticsData.EnterGame;
            Debug.Log($"Loading end. {_playerData.Donut}");
        }

        private void UpdateTexts()
        {
            _mediator.UpdateDonutScore(_playerData.Donut);
            _mediator.UpdateDonateScore(_playerData.Donate);
            _mediator.UpdateDonutPerClick(_playerData.StrengthClick * _playerData.FactorClick);
            _mediator.UpdateDonutPerSeconds(0);
            _mediator.UpdateLevelPlayer(_playerData.Level);
        }
    }
}