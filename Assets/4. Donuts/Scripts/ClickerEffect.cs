using System;
using _4._Donuts.Scripts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace _4._Donuts.Scripts
{
    public class ClickerEffect : MonoBehaviour
    {
        private GameObject _gameObject;
        private const float SPEED_NUMBER = 1f;
        private Vector2 _randomVector;
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _gameObject = gameObject;
            _text = _gameObject.GetComponent<TextMeshProUGUI>();
            Invoke(nameof(RemoveObject), 5);
        }

        private void Update()
        {
            transform.Translate(_randomVector * (Time.deltaTime * SPEED_NUMBER));
        }

        public void StartAnimation(string strengthClick)
        {
            _text.text = $"{strengthClick}<sprite=0>";
            _text.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            _randomVector = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
        }

        private void RemoveObject()
        {
            Destroy(_gameObject);
        }
    }
}