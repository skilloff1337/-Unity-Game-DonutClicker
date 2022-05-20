using _0._Localization.Scripts.Interfaces;
using _8._Tooltips.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _8._Tooltips.Scripts
{
    public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private ITooltipSystem _tooltipSystem;
        private ILocalizationSystem _localizationSystem;
        
        public string BodyText;
        public float PivotX;
        public float PivotY;

        [Inject]
        private void Constructor(ITooltipSystem tooltipSystem, ILocalizationSystem localizationSystem)
        {
            _tooltipSystem = tooltipSystem;
            _localizationSystem = localizationSystem;
        }

        public void Fabric(ITooltipSystem tooltipSystem, ILocalizationSystem localizationSystem)
        {
            if(_tooltipSystem != null && _localizationSystem != null)
                return;
            
            _tooltipSystem = tooltipSystem;
            _localizationSystem = localizationSystem;
        }

        private void ShowTooltip()
        {
            var body = _localizationSystem.TranslateWord(BodyText);
            _tooltipSystem.SetTextTooltip(body);
            _tooltipSystem.SetPivotTooltip(PivotX, PivotY);
            _tooltipSystem.ShowPanelTooltip(true);
        }

        private void HideTooltip()
        {
            _tooltipSystem.ShowPanelTooltip(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowTooltip();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideTooltip();
        }
    }
}