using System;
using System.Diagnostics;
using _0._Localization.Interfaces;
using _0._Localization.Lists;
using _4._Donuts.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace _4._Donuts.Scripts
{
    public class DonutConvertSystem : IDonutConvertSystem
    {
        private ILocalizationSystem _localizationSystem;
        
        private const int START_SHORT_NUMBER = 1_000_000;
        private readonly int _massiveLength = DonutConvertListName.EnglishNameNumbers.Length - 1;

        [Inject]
        private void Constructor(ILocalizationSystem localizationSystem)
        {
            _localizationSystem = localizationSystem;
        }

        public string ConvertNumber(double value)
        {
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
                return (value * isNegative).ToString("0,0,0");
            
            var index = -1;
            while (value >= 1000d)
            {
                if (index >= _massiveLength) 
                    break;

                value = value / 1000d;
                index++;
            }
            
            var additionallyText = _localizationSystem.CurrentLanguage switch
            {
                LanguagesType.Russian => DonutConvertListName.RussianNameNumbers[index],
                LanguagesType.English => DonutConvertListName.EnglishNameNumbers[index],
                _ => throw new ArgumentOutOfRangeException()
            };
            return $"{(value * isNegative):#.#} {additionallyText}";
        }
    }
}