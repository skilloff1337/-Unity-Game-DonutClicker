using _3._UI.Scripts.Interfaces;
using _4._Donuts.Scripts.Interfaces;
using _5._DataBase.Interfaces;
using UnityEngine;
using Zenject;

namespace _3._UI.Scripts
{
    public class Mediator : MonoBehaviour, IMediator
    {
        private ILineInformation _lineInformation;
        private IDonutConvertSystem _donutConvertSystem;
        private IPlayerData _playerData;
        
        [Inject]
        private void Constructor(ILineInformation lineInformation, IDonutConvertSystem donutConvertSystem, IPlayerData playerData)
        {
            _lineInformation = lineInformation;
            _donutConvertSystem = donutConvertSystem;
            _playerData = playerData;
        }

        public void UpdateDonutScore() => _lineInformation.UpdateDonutScore(_donutConvertSystem.ConvertNumber(_playerData.Donut));
        public void UpdateDonateScore() => _lineInformation.UpdateDonateScore(_donutConvertSystem.ConvertNumber(_playerData.Donate));
        public void UpdateDonutPerClick() => _lineInformation.UpdateDonutPerClick(_donutConvertSystem.ConvertNumber((_playerData.StrengthClick + _playerData.LevelData.LevelMultipleClick) * _playerData.X2DonutForClick 
            * _playerData.DonutLevel));
        public void UpdateDonutPerSeconds() => _lineInformation.UpdateDonutPerSeconds(_donutConvertSystem.ConvertNumber(_playerData.DonutPerSecond));
        public void UpdateLevelPlayer() => _lineInformation.UpdateLevelPlayer(_donutConvertSystem.ConvertNumber(_playerData.LevelData.Level));
        
    }
}