using _5._DataBase.Interfaces;

namespace _5._DataBase.Scripts
{
    public class SettingsData : ISettingsData
    {
        public float VolumeMusic { get; set; }
        public float VolumeSound { get; set; }
        public int AutoSave { get; set; }
    }
}