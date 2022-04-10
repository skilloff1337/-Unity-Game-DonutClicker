using _3._UI.Scripts.Interfaces;
using _4._Donuts.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace _3._UI.Scripts
{
    public class Mediator : MonoBehaviour
    {
        private ILineInformation _lineInformation;
        private IDonutConvertSystem _donutConvertSystem;
        
        [Inject]
        private void Constructor(ILineInformation lineInformation, IDonutConvertSystem donutConvertSystem)
        {
            _lineInformation = lineInformation;
            _donutConvertSystem = donutConvertSystem;
        }


        public void UpdateDonutScore(double value) => _lineInformation.UpdateDonutScore(_donutConvertSystem.ConvertNumber(value));
        public void UpdateDonateScore(double value) => _lineInformation.UpdateDonateScore(_donutConvertSystem.ConvertNumber(value));
        public void UpdateDonutPerClick(double value) => _lineInformation.UpdateDonutPerClick(_donutConvertSystem.ConvertNumber(value));
        public void UpdateDonutPerSeconds(double value) => _lineInformation.UpdateDonutPerSeconds(_donutConvertSystem.ConvertNumber(value));
        public void UpdateLevelPlayer(double value) => _lineInformation.UpdateLevelPlayer(_donutConvertSystem.ConvertNumber(value));
        
    }
}