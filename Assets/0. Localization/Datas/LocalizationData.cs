using System.Collections.Generic;

namespace _0._Localization.Datas
{
    public class LocalizationData
    {
        public string KeyWord;
        public string Translation;

        public LocalizationData(string keyWord, string translate)
        {
            KeyWord = keyWord;
            Translation = translate;
        }

        public LocalizationData()
        {
            
        }
    }
}