using System.Collections.Generic;
using _0._Localization.Lists;

namespace _0._Localization.Scripts
{
    public interface ILocalizationRepository
    {
        IDictionary<string,string> LoadWordsFromLanguage();
    }
}