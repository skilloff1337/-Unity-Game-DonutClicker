using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using _0._Localization.Scripts.Interfaces;
using _1._Logs.Lists;
using _1._Logs.Scripts.Interfaces;
using _11._Shop.Data;
using _11._Shop.Scripts;
using _12._Upgrade.Scripts;
using _13._Achievements.Scripts;
using _15._Notification.Scripts;
using _3._UI.Scripts;
using _3._UI.Scripts.Interfaces;
using _4._Donuts.Scripts;
using _4._Donuts.Scripts.Interfaces;
using _5._DataBase.Data;
using _5._DataBase.Interfaces;
using MongoDB.Bson;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Zenject;
using Debug = UnityEngine.Debug;
using Random = System.Random;

public class TestScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textFpsCounter;
    [SerializeField] private TextMeshProUGUI testText;
    [SerializeField] private Button buttonTest;

    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private GameObject[] _prefabsShop;
    [SerializeField] private GameObject _prefabClick;
    [SerializeField] private GameObject _prefabDonut;
    [SerializeField] private GameObject _prefabCritChance;
    [SerializeField] private GameObject _prefabCritValue;
    [SerializeField] private GameObject _prefabOfflineTime;
    [SerializeField] private GameObject _prefabOfflineProfit;

    private IMediator _mediator;
    private ILocalizationSystem _localizationSystem;
    private ILogSystem _logSystem;
    private IDonutConvertSystem _donutConvertSystem;
    private IPlayerData _playerData;
    private IPlayerDataManager _playerDataManager;
    private IShopSystem _shopSystem;
    private INotificationSystem _notification;
    private IRepository _mongo;

    [SerializeField] private AchievementsSystem _achievementsSystem;

    private static readonly Stopwatch _stopwatch = new Stopwatch();
    private static readonly Random _random = new Random();

    [Inject]
    private void Constructor(ILocalizationSystem localizationSystem, ILogSystem logSystem,
        IDonutConvertSystem donutConvertSystem, IMediator mediator, IPlayerData playerData,
        IPlayerDataManager playerDataManager, IShopSystem shopSystem, INotificationSystem notification, IRepository mongo)
    {
        _localizationSystem = localizationSystem;
        _logSystem = logSystem;
        _donutConvertSystem = donutConvertSystem;
        _mediator = mediator;
        _playerData = playerData;
        _playerDataManager = playerDataManager;
        _shopSystem = shopSystem;
        _notification = notification;
        _mongo = mongo;
    }


    private void Awake()
    {
     //   buttonTest.onClick.AddListener(Test);
    }

    private void Start()
    {
        StartCoroutine(FpsCounter());
    }

    private void OnApplicationQuit()
    {
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private IEnumerator FpsCounter()
    {
        while (true)
        {
            textFpsCounter.text = $"FPS: {(int) (1f / Time.unscaledDeltaTime)}";
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void Test(string nick)
    {
        var test = new string[] { "lox", "pidoras"};
        Debug.Log(test.Contains(nick));
        
    }
}