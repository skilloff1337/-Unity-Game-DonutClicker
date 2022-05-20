using System.Collections;
using _0._Localization.Scripts.Interfaces;
using _15._Notification.Scripts;
using _5._DataBase.Interfaces;
using _5._DataBase.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _4._Donuts.Scripts
{
    public class AntiClicker : MonoBehaviour
    {
        [SerializeField] private int _timerBan;
        [SerializeField] private int _clickPerSecondForBan;
        [SerializeField] private TextMeshProUGUI _textCPS;
        
        private INotificationSystem _notification;
        private ILocalizationSystem _local;
        private IStatisticsDataManager _stats;

        private float _nowClicks;
        private Button _gameObject;

        [Inject]
        private void Constructor(INotificationSystem notification, ILocalizationSystem local, IStatisticsDataManager stats)
        {
            _notification = notification;
            _local = local;
            _stats = stats;
        }
        private void Awake()
        {
            _gameObject = gameObject.GetComponent<Button>();
            _gameObject.onClick.AddListener(AddClick);
        }

        private void Start()
        {
            StartCoroutine(CheckAntiAutoClicker());
        }
        private void AddClick()
        {
            _nowClicks++;
        }

        private IEnumerator CheckAntiAutoClicker()
        {
            while (true)
            {
                _textCPS.text = _nowClicks == 0 ? string.Empty : $"{_nowClicks}";
                if (_nowClicks >= _clickPerSecondForBan)
                {
                    StartCoroutine(StartBan());
                }
                _nowClicks = 0;
                yield return new WaitForSeconds(1);
            }
        }

        private IEnumerator StartBan()
        {
            _stats.AddBansForAntiClicker();
            _gameObject.interactable = false;
            _notification.CreateNotification(_local.TranslateWord("ANTI_CLICKER_BAN"),10);
            yield return new WaitForSeconds(_timerBan);
            _gameObject.interactable = true;
        }
    }
}