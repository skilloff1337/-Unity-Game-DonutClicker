using UnityEngine;
using UnityEngine.UI;

namespace _3._UI.Scripts
{
    public class ButtonOpenPanel : MonoBehaviour
    {
        [SerializeField] private GameObject panelSettings;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ShowSettingsPanel);
        }
        private void ShowSettingsPanel()
        {
            panelSettings.SetActive(true);
        } 

    }
}