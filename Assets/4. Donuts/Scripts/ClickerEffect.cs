using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _4._Donuts.Scripts
{
    public class ClickerEffect : MonoBehaviour
    {
        private GameObject _gameObject;
        private Vector2 _randomVector;
        private TextMeshProUGUI _text;
        
        private const float SPEED_NUMBER = 1f;

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

        public void StartAnimation(string strengthClick,bool isCrit = false, int factor = 2)
        {
            _text.text = $"{strengthClick}<sprite=0>";
            _randomVector = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
            if (!isCrit) 
                return;
            
            _text.color = new Color(1,0,0,1);
            _text.text += $"  (X{factor})";
        }

        private void RemoveObject()
        {
            Destroy(_gameObject);
        }
    }
}