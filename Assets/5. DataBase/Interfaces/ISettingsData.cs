using _0._Localization.Lists;

namespace _5._DataBase.Interfaces
{
    public interface ISettingsData
    {
        float VolumeMusic { get; set; }
        float VolumeSound { get; set; }
        int AutoSave { get; set; }
        LanguagesType CurrentLanguages { get; set; }
    }
}