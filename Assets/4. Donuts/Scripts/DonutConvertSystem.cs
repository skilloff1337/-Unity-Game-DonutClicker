using System;
using _0._Localization.Lists;
using _4._Donuts.Scripts.Interfaces;
using _5._DataBase.Interfaces;
using Zenject;

namespace _4._Donuts.Scripts
{
    public class DonutConvertSystem : IDonutConvertSystem
    {
        private ISettingsData _settingsData;
        
        private const int START_SHORT_NUMBER = 1_000_000;
        private readonly int _massiveLength = DonutConvertListName.EnglishNameNumbers.Length - 1;

        [Inject]
        private void Constructor(ISettingsData settingsData)
        {
            _settingsData = settingsData;
        }

        public string ConvertNumber(double value)
        {
            if (double.IsInfinity(value))
                return "Infinity";
            if (value == 0) 
                return "0";

            var isNegative = 1;

            if (value < 0)
            {
                isNegative = -1;
                value = value * -1;
            }

            if (value < 999) 
                return $"{value}";
            if (value < START_SHORT_NUMBER)
                return $"{value * isNegative:#,#,#} ";
            
            var index = -1;
            while (value >= 1000d)
            {
                if (index >= _massiveLength) 
                    break;

                value = value / 1000d;
                index++;
            }
            
            var additionallyText = _settingsData.CurrentLanguages switch
            {
                LanguagesType.Russian => DonutConvertListName.RussianNameNumbers[index],
                LanguagesType.English => DonutConvertListName.EnglishNameNumbers[index],
                _ => throw new ArgumentOutOfRangeException()
            };
            return $"{(value * isNegative):#.#} {additionallyText}";
        }
    }
}