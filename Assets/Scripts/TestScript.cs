using System;
using _0._Localization.Interfaces;
using _1._Logs.Lists;
using _1._Logs.Scripts.Interfaces;
using _3._UI.Scripts;
using _3._UI.Scripts.Interfaces;
using _4._Donuts.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TestScript : MonoBehaviour
{
    [SerializeField] private Button buttonTest;
    [SerializeField] private Mediator mediator;

    private ILocalizationSystem _localizationSystem;
    private ILogSystem _logSystem;
    private IDonutConvertSystem _donutConvertSystem;

    [Inject]
    private void Constructor(ILocalizationSystem localizationSystem, ILogSystem logSystem,
        IDonutConvertSystem donutConvertSystem)
    {
        _localizationSystem = localizationSystem;
        _logSystem = logSystem;
        _donutConvertSystem = donutConvertSystem;
    }


    private void Awake()
    {
        buttonTest.onClick.AddListener(Test);
        Screen.SetResolution(1920, 1080, true, 144);
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
        _localizationSystem.SwitchLanguage();
    }
}