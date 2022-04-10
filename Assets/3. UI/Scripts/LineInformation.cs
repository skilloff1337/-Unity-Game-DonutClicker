using _3._UI.Scripts.Interfaces;
using TMPro;
using UnityEngine;

namespace _3._UI.Scripts
{
    public class LineInformation : MonoBehaviour, ILineInformation
    {
        [SerializeField] private TextMeshProUGUI donutScore;
        [SerializeField] private TextMeshProUGUI donateScore;
        [SerializeField] private TextMeshProUGUI donutPerSecond;
        [SerializeField] private TextMeshProUGUI donutPerClick;
        [SerializeField] private TextMeshProUGUI levelScore;

        public void UpdateDonutScore(string text)
        {
            donutScore.text = $"{text}<sprite=0>";
        }
        public void UpdateDonateScore(string text)
        {
            donateScore.text = $"{text}<sprite=1>";
        }
        public void UpdateDonutPerSeconds(string text)
        {
            donutPerSecond.text = $"{text}<sprite=2>";
        }   
        public void UpdateDonutPerClick(string text)
        {
            donutPerClick.text = $"{text}<sprite=3>";
        }      
        public void UpdateLevelPlayer(string text)
        {
            levelScore.text = $"{text}<sprite=4>";
        }
    }
}