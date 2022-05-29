using System;
using System.IO;
using _5._DataBase.Interfaces;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace _5._DataBase.Scripts.Load
{
    public class LoadingSettings : MonoBehaviour
    {
        private ISettingsData _settingsData;

        private string _pathSettingsDataBuild;

        private const string PATH_SETTINGS_DATA =
            "F:/Project/My New Project 2022/Unity Project/Donut Clicker/Clicker Donut/Assets/" +
            "5. DataBase/DataPlayer/PlayerSettings.json";

        [Inject]
        private void Constructor(ISettingsData settingsData)
        {
            _settingsData = settingsData;
        }

        private void Awake()
        {
            _pathSettingsDataBuild = Path.Combine(Application.dataPath, "DataPlayer","PlayerSettings.json");
        }

        public void LoadSettingsFile()
        {
            if (string.IsNullOrEmpty(_pathSettingsDataBuild))
                _pathSettingsDataBuild = Path.Combine(Application.dataPath, "DataPlayer","PlayerSettings.json");
#if UNITY_EDITOR
            if (!File.Exists(PATH_SETTINGS_DATA)) return;
            var lines = File.ReadAllText(PATH_SETTINGS_DATA);
            var settings = JsonConvert.DeserializeObject<SettingsData>(lines);
            SettingsFile(settings);
#else
            if(!File.Exists(_pathSettingsDataBuild)) return;
            var lines = File.ReadAllText(_pathSettingsDataBuild);
            var settings = JsonConvert.DeserializeObject<SettingsData>(lines);
            SettingsFile(settings);
#endif
        }

        private void SettingsFile(ISettingsData settings)
        {
            _settingsData.VolumeMusic = settings.VolumeMusic;
            _settingsData.VolumeSound = settings.VolumeSound;
            _settingsData.AutoSave = settings.AutoSave;
            _settingsData.CurrentLanguages = settings.CurrentLanguages;
        }
    }
}