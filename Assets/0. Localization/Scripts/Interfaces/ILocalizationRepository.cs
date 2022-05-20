using System.Collections.Generic;

namespace _0._Localization.Scripts.Interfaces
{
    public interface ILocalizationRepository
    {
        Dictionary<string,string> LoadWordsFromLanguage();
    }
}