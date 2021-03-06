using System;
using System.IO;
using _5._DataBase.Interfaces;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace _5._DataBase.Scripts.Save
{
    public class SavingSettings : MonoBehaviour
    {
        private ISettingsData _settingsData;

        private string _pathSettingsDataBuild;

        private const string PATH_SETTINGS_DATA_DEV =
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

        private void OnApplicationQuit()
        {
            SavingSettingsFile();
        }

        private void SavingSettingsFile()
        {
            var save = JsonConvert.SerializeObject(SettingsFile());
#if UNITY_EDITOR
            File.WriteAllText(PATH_SETTINGS_DATA_DEV, save);
#else
             File.WriteAllText(_pathSettingsDataBuild,save);
#endif
        }

        private SettingsData SettingsFile()
        {
            var settings = new SettingsData
            {
                VolumeMusic = _settingsData.VolumeMusic,
                VolumeSound = _settingsData.VolumeSound,
                AutoSave = _settingsData.AutoSave,
                CurrentLanguages = _settingsData.CurrentLanguages
            };
            return settings;
        }
    }
}