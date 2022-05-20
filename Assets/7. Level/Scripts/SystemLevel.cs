using _3._UI.Scripts.Interfaces;
using _5._DataBase.Interfaces;
using _7._Level.Interfaces;
using UnityEngine;
using Zenject;

namespace _7._Level.Scripts
{
    public class SystemLevel : MonoBehaviour, ISystemLevel
    {
        private IPlayerData _playerData;
        private ISystemExp _systemExp;
        private IMediator _mediator;
        private ILevelDataManager _levelDataManager;

        [Inject]
        private void Constructor(IPlayerData playerData, ISystemExp systemExp, IMediator mediator,
            ILevelDataManager levelDataManager)
        {
            _playerData = playerData;
            _systemExp = systemExp;
            _mediator = mediator;
            _levelDataManager = levelDataManager;
        }

        public void AddExp()
        {
            if (_playerData.LevelData.Level == _playerData.LevelData.MaxLevel)
                return;

            _levelDataManager.AddExpClick();

            CheckExpForNewLevel();
        }

        private void CheckExpForNewLevel()
        {
            if (_playerData.LevelData.NeedExpForNextLevel >= double.MaxValue)
                _systemExp.NeedExpForNextLevel(_playerData.LevelData.Level);

            if (_playerData.LevelData.NeedExpForNextLevel > _playerData.LevelData.Exp)
                return;

            _levelDataManager.UpLevel();
            _mediator.UpdateLevelPlayer();
        }
    }
}