using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _2._Loadings.Scripts
{
    public class LoadingSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _textProcentLoading;
        [SerializeField] private TextMeshProUGUI _textLoading;

        [Range(1, 50)] [SerializeField] private float _speedLoading;

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
        }

        private void Start()
        {
            StartCoroutine(Loading());
            StartCoroutine(SetText());
        }

        private IEnumerator SetText()
        {
            var num = 1;
            var mainText = _textLoading.text;
            while (_slider.value < 100f)
            {
                if (num > 0 && num < 4)
                    _textLoading.text += ".";
                if (num == 4)
                {
                    _textLoading.text = mainText;
                    num = 0;
                }
                num++;
                yield return new WaitForSeconds(1);
            }
        }

        private IEnumerator Loading()
        {
            while (_slider.value < 100f)
            {
                _slider.value += _speedLoading * Time.deltaTime;
                _textProcentLoading.text = $"{_slider.value:0.0}%";
                yield return new WaitForEndOfFrame();
            }
            _gameObject.SetActive(false);
        }
    }
}