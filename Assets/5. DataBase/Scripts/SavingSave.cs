using System;
using System.IO;
using _5._DataBase.Data;
using _5._DataBase.Interfaces;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace _5._DataBase.Scripts
{
    public class SavingSave : MonoBehaviour
    {
        private IPlayerData _playerData;

        private const string PATH_SAVE_DATA =
            "F:/Project/My New Project 2022/Unity Project/Donut Clicker/Clicker Donut/Assets/" +
            "5. DataBase/DataPlayer/PlayerSave.json";

        [Inject]
        private void Constructor(IPlayerData playerData)
        {
            _playerData = playerData;
        }
        private void OnApplicationQuit()
        {
            SavingSaveFile();
        }

        private void SavingSaveFile()
        {
            Debug.Log($"SAVE!");
            var save = JsonConvert.SerializeObject(SaveFile());
            File.WriteAllText(PATH_SAVE_DATA,save);
        }

        private PlayerData SaveFile()
        {
            var save = new PlayerData
            {
                Donut = _playerData.Donut,
                Donate = _playerData.Donate,
                StrengthClick = _playerData.StrengthClick,
                FactorClick = _playerData.FactorClick,
                DonutLevel = _playerData.DonutLevel,
                Level = _playerData.Level,
                Exp = _playerData.Exp,
                ChanceCrit = _playerData.ChanceCrit,
                StatisticsData =
                {
                    Clicks = 100, //_playerData.StatisticsData.Clicks;
                    EnterGame = 100 //_playerData.StatisticsData.EnterGame;
                }
            };
            return save;
        }
    }
}