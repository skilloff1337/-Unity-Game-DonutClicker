using UnityEngine;
using UnityEngine.UI;

namespace _16._Top_Players.Scripts
{
    public class TopButtonsAndPanels : MonoBehaviour
    {
        [SerializeField] private GameObject[] _panels;
        [SerializeField] private Button[] _buttons;

        public void ClickButtonDonut(int value)
        {
            for (var i = 0; i < _panels.Length; i++)
            {
                if (i == value)
                {
                    _buttons[i].interactable = false;
                    _panels[i].SetActive(true);
                }
                else
                {
                    _buttons[i].interactable = true;
                    _panels[i].SetActive(false);
                }
            }

        }
    }
}