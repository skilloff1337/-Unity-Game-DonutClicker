using _8._Tooltips.Interfaces;
using TMPro;
using UnityEngine;

namespace _8._Tooltips.Scripts
{
    public class TooltipSystem : MonoBehaviour, ITooltipSystem
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TextMeshProUGUI _bodyText;
        
        private RectTransform _rectPanel;
        private Camera _camera;
        private void Awake()
        {
            _rectPanel = _panel.GetComponent<RectTransform>();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!_panel.activeInHierarchy)
                return;
            
            var posMouse = _camera.ScreenToWorldPoint(Input.mousePosition);
            _panel.transform.position = new Vector3(posMouse.x, posMouse.y, 1);
        }

        public void SetTextTooltip(string bodyText)
        {
            _bodyText.text = bodyText;
        }

        public void SetPivotTooltip(float pivotX,float pivotY)
        {
            _rectPanel.pivot = new Vector2(pivotX,pivotY);
        }

        public void ShowPanelTooltip(bool value)
        {
            _panel.SetActive(value);
        }
    }
}