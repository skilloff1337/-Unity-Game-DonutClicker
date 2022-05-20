namespace _0._Localization.Scripts.Interfaces
{
    public interface ILocalizationSystem
    {
        string TranslateWord(string textID);
        void SetAutomaticText();
        void LoadingLanguages();
    }
}