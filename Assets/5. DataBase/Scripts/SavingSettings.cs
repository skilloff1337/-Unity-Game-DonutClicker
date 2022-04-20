using System.IO;
using _5._DataBase.Interfaces;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace _5._DataBase.Scripts
{
    public class SavingSettings : MonoBehaviour
    {
        private const string PATH_SETTINGS_DATA =
            "F:/Project/My New Project 2022/Unity Project/Donut Clicker/Clicker Donut/Assets/" +
            "5. DataBase/DataPlayer/PlayerSettings.json";
        
        private ISettingsData _settingsData;
        
        [Inject]
        private void Constructor(ISettingsData settingsData)
        {
            _settingsData = settingsData;
        }
        
        private void OnApplicationQuit()
        {
            SavingSettingsFile();
        }
        
        private void SavingSettingsFile()
        {
            Debug.Log($"SAVE!");
            var save = JsonConvert.SerializeObject(SettingsFile());
            File.WriteAllText(PATH_SETTINGS_DATA,save);
        }
        
        private SettingsData SettingsFile()
        {
            var settings = new SettingsData
            {
                VolumeMusic = _settingsData.VolumeMusic,
                VolumeSound = _settingsData.VolumeSound,
                AutoSave = _settingsData.AutoSave
            };
            return settings;
        }
    }
}