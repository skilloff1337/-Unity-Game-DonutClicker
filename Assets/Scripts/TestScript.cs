using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TestScript : MonoBehaviour
    {
        [SerializeField] private Button _buttonTest;

        private void Awake()
        {
            _buttonTest.onClick.AddListener(Test);
        }

        private void Update()
        {
            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        }

        private void Test()
        {
   
        }
    }
}