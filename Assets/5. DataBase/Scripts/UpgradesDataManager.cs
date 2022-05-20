using _5._DataBase.Data;
using _5._DataBase.Interfaces;
using UnityEngine;
using Zenject;

namespace _5._DataBase.Scripts
{
    public class UpgradesDataManager : MonoBehaviour, IUpgradesDataManager
    {
        private IPlayerData _playerData;
        private UpgradesData _upgradesData;

        [Inject]
        private void Constructor(IPlayerData playerData)
        {
            _playerData = playerData;
            _upgradesData = _playerData.UpgradesData;

        }
        public void SetUpgradeDonutCost(double value) => _upgradesData.UpgradeDonutCost = value;
        
        public void SetChanceCritCost(double value) => _upgradesData.UpgradeChanceCritCost = value;
        public void SetValueCritCost(double value) => _upgradesData.UpgradeValueCritCost = value;
        public void AddChanceCritLevel() => _upgradesData.UpgradeChanceCritLevel++;
        public void AddCritValueLevel() => _upgradesData.UpgradeValueCritLevel++;
        
        public void AddUpgradeClickLevel() => _upgradesData.UpgradeClickLevel++;
        public void SetUpgradeClickCost(double value) => _upgradesData.UpgradeClickCost = value;
        
        
        public void SetUpgradeOfflineTimeCost(double value) => _upgradesData.UpgradeOfflineTimeCost = value;
        public void SetUpgradeOfflineProfitRatioCost(double value) => _upgradesData.UpgradeProfitRatioCost = value;
        public void AddUpgradeOfflineTimeLevel() => _upgradesData.UpgradeOfflineTimeLevel++;
        public void AddUpgradeOfflineProfitRatioLevel() => _upgradesData.UpgradeProfitRatioLevel++;
    }
}