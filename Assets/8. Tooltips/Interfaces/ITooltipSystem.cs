namespace _8._Tooltips.Interfaces
{
    public interface ITooltipSystem
    {
        void SetTextTooltip(string bodyText);
        void ShowPanelTooltip(bool value);
        void SetPivotTooltip(float pivotX, float pivotY);
    }
}