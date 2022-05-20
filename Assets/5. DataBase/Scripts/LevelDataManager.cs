using _1._Logs.Lists;
using _1._Logs.Scripts.Interfaces;
using _3._UI.Scripts.Interfaces;
using _5._DataBase.Interfaces;
using _7._Level.Interfaces;
using _7._Level.Scripts;
using UnityEngine;
using Zenject;

namespace _5._DataBase.Scripts
{
    public class LevelDataManager : MonoBehaviour, ILevelDataManager
    {
        private IMediator _mediator;
        private IPlayerData _playerData;
        private ILogSystem _logSystem;
        private ISystemExp _systemExp;
        private IStatisticsDataManager _stats;

        [Inject]
        private void Constructor(IPlayerData playerData, IMediator mediator,
            ILogSystem logSystem, ISystemExp systemExp, IStatisticsDataManager stats)
        {
            _playerData = playerData;
            _mediator = mediator;
            _logSystem = logSystem;
            _systemExp = systemExp;
            _stats = stats;
        }

        public void UpLevel()
        {
            _playerData.LevelData.Level++;
            _playerData.LevelData.Exp = 0;
            AddMultipleClick();
            _mediator.UpdateDonutPerClick();

            _playerData.LevelData.NeedExpForNextLevel = _systemExp.NeedExpForNextLevel(_playerData.LevelData.Level);

            _logSystem.AddLog(LogsType.Level, $"Player {_playerData.NickName} up level, new level -> {_playerData.LevelData.Level}");
        }

        public void AddExp(double value)
        {
            _playerData.LevelData.Exp += value;
            _stats.AddEarnedExp(value);
        }

        public void AddExpClick()
        {
            var value = _playerData.LevelData.ExpForClick * _playerData.LevelData.X2ExpForClick;
            _playerData.LevelData.Exp += value;
            _stats.AddEarnedExp(value);
        }

        public void AddMultipleClick() => _playerData.LevelData.LevelMultipleClick += 2;

        public void AddValueExpPerClick(double value) => _playerData.LevelData.ExpForClick += value;

        public void SetX2ExpForClick(bool value) => _playerData.LevelData.X2ExpForClick = value ? 2 : 1;
    }
}