using UnityEngine;
using UnityEngine.UI;

namespace _3._UI.Scripts
{
    public class ButtonClosePanel : MonoBehaviour
    {
        private Button _button;
        private GameObject _panel;

        private void Awake()
        {
            _panel = gameObject;
            _button = GameObject.Find("Button_Close").GetComponent<Button>();
            _button.onClick.AddListener(HidePanel);
        }

        private void HidePanel()
        {
            _panel.SetActive(false);
        }
    }
}