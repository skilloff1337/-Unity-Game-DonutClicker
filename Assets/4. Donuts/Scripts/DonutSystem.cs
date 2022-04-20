using System;
using _3._UI.Scripts;
using _4._Donuts.Scripts.Interfaces;
using _5._DataBase.Interfaces;
using UnityEngine;
using Zenject;

namespace _4._Donuts.Scripts
{
    public class DonutSystem : MonoBehaviour
    {
        private IMediator _mediator;
        private IPlayerData _playerData;
        private IDonutConvertSystem _donutConvertSystem;

        [Inject]
        private void Constructor(IPlayerData playerData, IDonutConvertSystem donutConvertSystem, IMediator mediator)
        {
            _playerData = playerData;
            _donutConvertSystem = donutConvertSystem;
            _mediator = mediator;
        }

        public bool AddDonuts(double value)
        {
            if (_playerData.Donut + value < 0 || double.IsInfinity(_playerData.Donut))
            {
                Debug.Log($"Unable to credit donuts,[{value}], there is [{_playerData.Donut}]");
                return false;
            }
            _playerData.Donut += value;
            Debug.Log($"Add donuts [{value}], new donuts value [{_playerData.Donut}]");
            _mediator.UpdateDonutScore(_playerData.Donut);
            return true;
        }

        public bool DelDonuts(double value)
        {
            if (_playerData.Donut - value < 0)
            {
                Debug.Log($"Not enough donuts, needs [{value}], there is [{_playerData.Donut}]");
                return false;
            }
            _playerData.Donut -= value;
            Debug.Log($"Del donuts [{value}], new donuts value [{_playerData.Donut}]");
            _mediator.UpdateDonutScore(_playerData.Donut);
            return true;
        }
}
}