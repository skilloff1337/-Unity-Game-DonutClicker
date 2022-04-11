using System;
using System.IO;
using _5._DataBase.Data;
using _5._DataBase.Interfaces;
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
            SavingFile();
        }

        private void SavingFile()
        {
            Debug.Log($"SAVE!");
            var save = JsonUtility.ToJson(SaveFile());
            File.WriteAllText(PATH_SAVE_DATA,save);
        }

        private Save SaveFile()
        {
            var save = new Save();
            save.Donut = _playerData.Donut;
            save.Donate = _playerData.Donate;
            save.StrengthClick = _playerData.StrengthClick;
            save.FactorClick = _playerData.FactorClick;
            save.DonutLevel = _playerData.DonutLevel;
            save.Level = _playerData.Level;
            save.Exp = _playerData.Exp;
            return save;
        }
    }
}