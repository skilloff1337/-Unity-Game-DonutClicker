using _4._Donuts.Scripts.Interfaces;
using _5._DataBase.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace _4._Donuts.Scripts
{
    public class ClickerSystem : MonoBehaviour
    {
        [SerializeField] private DonutSystem donutSystem;
        [SerializeField] private GameObject prefab;
        [SerializeField] private GameObject parent;

        private IPlayerData _playerData;
        private IDonutConvertSystem _donutConvertSystem;

        private Button _button;
        private Animation _animation;

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
            _animation = GetComponent<Animation>();
        }

        private void OnClickDonut()
        {
            DonutsPerClick();
            StartAnimation();
        }

        private void DonutsPerClick()
        {
            var obj = Instantiate(prefab, parent.transform).GetComponent<ClickerEffect>();
            var crit = RandomCritDamage();
            var donutPerClick = (_playerData.StrengthClick * _playerData.FactorClick) * _playerData.DonutLevel;

            if (crit)
                donutPerClick *= 2;

            donutSystem.AddDonuts(donutPerClick);
            obj.StartAnimation(_donutConvertSystem.ConvertNumber(donutPerClick), crit);
        }

        private bool RandomCritDamage() => Random.Range(0, 100) < _playerData.ChanceCrit;

        private void StartAnimation()
        {
            _animation.Play();
        }
    }
}