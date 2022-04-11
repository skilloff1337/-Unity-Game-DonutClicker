using System;
using System.IO;
using _3._UI.Scripts;
using _5._DataBase.Data;
using _5._DataBase.Interfaces;
using UnityEngine;
using Zenject;

namespace _5._DataBase.Scripts
{
    public class LoadingSave : MonoBehaviour
    {

        [SerializeField] private Mediator _mediator;
        private IPlayerData _playerData;

        private const string PATH_SAVE_DATA =
            "F:/Project/My New Project 2022/Unity Project/Donut Clicker/Clicker Donut/Assets/" +
            "5. DataBase/DataPlayer/PlayerSave.json";

        [Inject]
        private void Constructor(IPlayerData playerData)
        {
            _playerData = playerData;
        }

        private void Awake()
        {
            LoadFile();
        }

        private void LoadFile()
        {
            Debug.Log("LOAD");
            var lines = File.ReadAllText(PATH_SAVE_DATA);
            var save = new Save();
            JsonUtility.FromJsonOverwrite(lines, save);
            SaveFile(save);
            UpdateTexts();
        }

        private void SaveFile(Save save)
        {
            _playerData.Donut = save.Donut;
            _playerData.Donate = save.Donate;
            _playerData.StrengthClick = save.StrengthClick;
            _playerData.FactorClick = save.FactorClick;
            _playerData.DonutLevel = save.DonutLevel;
            _playerData.Level = save.Level;
            _playerData.Exp = save.Exp;
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