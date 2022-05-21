using _4._Donuts.Scripts.Interfaces;
using _5._DataBase.Interfaces;
using _5._DataBase.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace _4._Donuts.Scripts
{
    public class ClickerSystem : MonoBehaviour, IClickerSystem
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private GameObject _parent;
        [SerializeField] private Sprite[] _donutsImages;

        private IPlayerData _playerData;
        private IDonutConvertSystem _donutConvertSystem;
        private IPlayerDataManager _playerDataManager;
        private IStatisticsDataManager _stats;

        private Button _button;
        private Animation _animation;
        private Image _imageDonut;

        [Inject]
        private void Constructor(IPlayerData playerData, IDonutConvertSystem donutConvertSystem,
            IPlayerDataManager playerDataManager, IStatisticsDataManager statisticsDataManager)
        {
            _playerData = playerData;
            _donutConvertSystem = donutConvertSystem;
            _playerDataManager = playerDataManager;
            _stats = statisticsDataManager;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClickDonut);
            _animation = GetComponent<Animation>();
            _imageDonut = GetComponent<Image>();
        }

        private void OnClickDonut()
        {
            DonutsPerClick();
            StartAnimation();
        }

        private void DonutsPerClick()
        {
            var obj = Instantiate(_prefab, _parent.transform).GetComponent<ClickerEffect>();
            var crit = RandomCritDamage();
            var donutPerClick = (_playerData.StrengthClick + _playerData.LevelData.LevelMultipleClick)
                                * _playerData.DonutLevel * _playerData.X2DonutForClick ;

            if (crit)
                donutPerClick *= _playerData.ValueCrit == 0 ? 1 : _playerData.ValueCrit;

            _playerDataManager.AddDonuts(donutPerClick);
            _stats.AddClicks();
            _stats.AddClicksCurrentSession();
            _stats.AddEarnedWithClicks(donutPerClick);
            obj.StartAnimation(_donutConvertSystem.ConvertNumber(donutPerClick), crit, _playerData.ValueCrit);
        }

        private bool RandomCritDamage() => Random.Range(0, 100) < _playerData.ChanceCrit;

        private void StartAnimation()
        {
            _animation.Play();
        }

        public void LoadSpriteDonut()
        {
            if (_playerData.DonutLevel > _playerData.MaxDonutLevel || _playerData.DonutLevel <= 1)
                return;
            if(_imageDonut == null)
                _imageDonut = GetComponent<Image>();
            
            var idDonut = _playerData.DonutLevel - 1;
            _imageDonut.sprite = _donutsImages[idDonut];
        }
    }
}