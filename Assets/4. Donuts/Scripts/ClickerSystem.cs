using System;
using _3._UI.Scripts;
using _4._Donuts.Scripts.Interfaces;
using _5._DataBase.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _4._Donuts.Scripts
{
    public class ClickerSystem : MonoBehaviour
    {
        [SerializeField] private Mediator _mediator;
        [SerializeField] private GameObject prefab;
        [SerializeField] private GameObject parent;
        
        private Button _button;
        private IPlayerData _playerData;
        private IDonutConvertSystem _donutConvertSystem;
        [Inject]
        private void Constructor(IPlayerData playerData, IDonutConvertSystem donutConvertSystem)
        {
            _playerData = playerData;
            _donutConvertSystem = donutConvertSystem;
        }
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClickDonut);
        }

        private void OnClickDonut()
        {
            DonutsPerClick();
            CreatePrefab();
            _mediator.UpdateDonutScore(_playerData.Donut);
        }

        private void DonutsPerClick()
        {
            _playerData.Donut += (_playerData.StrengthClick * _playerData.FactorClick) * _playerData.DonutLevel;
        }
        private void CreatePrefab()
        {
           var obj = Instantiate(prefab,parent.transform).GetComponent<ClickerEffect>();
           obj.StartAnimation(_donutConvertSystem.ConvertNumber(_playerData.StrengthClick * _playerData.FactorClick));
        }
    }
}