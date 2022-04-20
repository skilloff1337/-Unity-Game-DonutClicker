using System;
using _0._Localization.Interfaces;
using _1._Logs.Lists;
using _1._Logs.Scripts.Interfaces;
using _3._UI.Scripts;
using _3._UI.Scripts.Interfaces;
using _4._Donuts.Scripts;
using _4._Donuts.Scripts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TestScript : MonoBehaviour
{

    private IMediator _mediator;
    [SerializeField] private DonutSystem donutSystem;

    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] private Button buttonTest;
    
    private ILocalizationSystem _localizationSystem;
    private ILogSystem _logSystem;
    private IDonutConvertSystem _donutConvertSystem;

    [Inject]
    private void Constructor(ILocalizationSystem localizationSystem, ILogSystem logSystem,
        IDonutConvertSystem donutConvertSystem, IMediator mediator)
    {
        _localizationSystem = localizationSystem;
        _logSystem = logSystem;
        _donutConvertSystem = donutConvertSystem;
        _mediator = mediator;
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

        _textMeshProUGUI.text = $"FPS: {(int) (1f / Time.unscaledDeltaTime)}";
    }

    private void Test()
    {
        //donutSystem.DelDonuts(20);
        _mediator.UpdateDonateScore(100);
        //    _localizationSystem.SwitchLanguage();
    }
}