namespace _3._UI.Scripts
{
    public interface IMediator
    {
        void UpdateDonutScore(double value);
        void UpdateDonateScore(double value);
        void UpdateDonutPerClick(double value);
        void UpdateDonutPerSeconds(double value);
        void UpdateLevelPlayer(double value);
    }
}