using System.IO;
using _5._DataBase.Interfaces;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace _5._DataBase.Scripts
{
    public class LoadingSettings : MonoBehaviour
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

        private void Awake()
        {
            LoadSettingsFile();
        }
        
        private void LoadSettingsFile()
        {
            if(!File.Exists(PATH_SETTINGS_DATA)) return;
            var lines = File.ReadAllText(PATH_SETTINGS_DATA);
            var settings = JsonConvert.DeserializeObject<SettingsData>(lines);
            SettingsFile(settings);
        }
        
        private void SettingsFile(SettingsData settings)
        {
            _settingsData.VolumeMusic = settings.VolumeMusic;
            _settingsData.VolumeSound = settings.VolumeSound;
            _settingsData.AutoSave = settings.AutoSave;
        }
    }
}