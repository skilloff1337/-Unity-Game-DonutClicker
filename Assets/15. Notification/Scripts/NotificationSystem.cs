using System.Collections;
using System.Collections.Generic;
using _15._Notification.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _15._Notification.Scripts
{
    public class NotificationSystem : MonoBehaviour, INotificationSystem
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _bodyText;
        [SerializeField] private TextMeshProUGUI _sliderText;
        [SerializeField] private TextMeshProUGUI _inQueue;
        
        private float _timer;
        private bool _isShow;
        private readonly Queue<NotificationData> _notificationsQueue = new Queue<NotificationData>();

        private void Update()
        {
            if (_gameObject.activeSelf == false)
                return;

            _slider.value -= 1 * Time.deltaTime;
            _sliderText.text = $"{_slider.value:0.0}";
        }

        public void CreateNotification(string text, float timer = 5)
        {
            _notificationsQueue.Enqueue(new NotificationData(text, timer));
            _inQueue.text = _notificationsQueue.Count == 0 ? " " : $"{_notificationsQueue.Count}";
            StartNotification();
        }

        private void  StartNotification()
        {
            if (_notificationsQueue.Count == 0 || _isShow)
                return;
            
            var notification = _notificationsQueue.Dequeue();
            _bodyText.text = notification.Text;
            _inQueue.text = _notificationsQueue.Count == 0 ? " " : $"{_notificationsQueue.Count}";
            _timer = notification.Timer;
            _slider.maxValue = _timer;
            _slider.value = _slider.maxValue;
            _sliderText.text = $"{_timer}";
            StartCoroutine(ShowNotification());
        }

        private IEnumerator ShowNotification()
        {
            _isShow = true;
            _gameObject.SetActive(true);
            yield return new WaitForSeconds(_timer);
            _gameObject.SetActive(false);
            _isShow = false;
            StartNotification();
        }
    }
}