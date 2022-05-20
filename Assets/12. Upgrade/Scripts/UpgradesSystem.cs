using _12._Upgrade.Scripts.Upgrades;
using UnityEngine;

namespace _12._Upgrade.Scripts
{
    public class UpgradesSystem : MonoBehaviour
    {
        [SerializeField] private GameObject[] _prefabsShopItems;
        [SerializeField] private GameObject _prefabClick;
        [SerializeField] private GameObject _prefabDonut;
        [SerializeField] private GameObject _prefabCritChance;
        [SerializeField] private GameObject _prefabCritValue;
        [SerializeField] private GameObject _prefabOfflineTime;
        [SerializeField] private GameObject _prefabOfflineProfit;

        public void UpdatePrefabs()
        {
            foreach (var prefab in _prefabsShopItems)
            {
                prefab.GetComponent<UpgradeShopItems>().UpdatePrefab();
            }
            _prefabClick.GetComponent<UpgradeClickStrength>().UpdatePrefab();
            _prefabDonut.GetComponent<UpgradeDonut>().UpdatePrefab();
            _prefabCritChance.GetComponent<UpgradeCritChance>().UpdatePrefab();
            _prefabCritValue.GetComponent<UpgradeCritValue>().UpdatePrefab();
            _prefabOfflineTime.GetComponent<UpgradeOfflineTime>().UpdatePrefab();
            _prefabOfflineProfit.GetComponent<UpgradeOfflineProfit>().UpdatePrefab();
        }

    }
}