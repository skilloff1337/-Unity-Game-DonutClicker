using System;
using _0._Localization.Lists;
using _5._DataBase.Interfaces;

namespace _5._DataBase.Scripts
{
    public class SettingsData : ISettingsData
    {
        public float VolumeMusic { get; set; } = 0.5f;
        public float VolumeSound { get; set; } = 0.5f;
        public int AutoSave { get; set; } = 300;
        public LanguagesType CurrentLanguages { get; set; } = LanguagesType.English;
    }
}