using _0._Localization.Lists;

namespace _0._Localization.Interfaces
{
    public interface ILocalizationSystem
    {
        string TranslateWord(string textID);
        void SwitchLanguage();
        void SetAutomaticText();
        LanguagesType CurrentLanguage { get; }
    }
}