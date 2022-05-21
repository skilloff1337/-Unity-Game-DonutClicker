using System;
using System.Collections;
using _0._Localization.Scripts.Interfaces;
using _13._Achievements.Data;
using _14._Quests.Data;
using _15._Notification.Scripts;
using _3._UI.Scripts.Interfaces;
using _4._Donuts.Scripts.Interfaces;
using _5._DataBase.Interfaces;
using UnityEngine;
using Zenject;

namespace _5._DataBase.Scripts
{
    public class PlayerDataManager : MonoBehaviour, IPlayerDataManager
    {
        private IMediator _mediator;
        private IPlayerData _playerData;
        private IClickerSystem _clickerSystem;
        private IOfflineBonus _offlineBonus;
        private ILevelDataManager _levelManager;
        private IStatisticsDataManager _stats;
        private INotificationSystem _notification;
        private ILocalizationSystem _local;


        private bool _isCoroutineRun;

        [Inject]
        private void Constructor(IPlayerData playerData, IMediator mediator,
            IClickerSystem clickerSystem, IOfflineBonus offlineBonus, ILevelDataManager levelDataManager,
            IStatisticsDataManager stats, INotificationSystem notification, ILocalizationSystem local)
        {
            _playerData = playerData;
            _mediator = mediator;
            _clickerSystem = clickerSystem;
            _offlineBonus = offlineBonus;
            _levelManager = levelDataManager;
            _stats = stats;
            _notification = notification;
            _local = local;
        }

        private void Start()
        {
            RestartCoroutineDps();
        }

        private void RestartCoroutineDps()
        {
            if (_isCoroutineRun)
            {
                StopCoroutine(DonutPerSeconds());
                _isCoroutineRun = false;
                StartCoroutine(DonutPerSeconds());
                return;
            }

            StartCoroutine(DonutPerSeconds());
        }

        public void GiveOfflineDonuts()
        {
            if (_playerData.OfflineTime <= 0 && _playerData.ProfitRatio <= 0)
                return;
            

            var donuts = _offlineBonus.CountOfflineBonus(_playerData.StatisticsData.LastLogin, _playerData.OfflineTime,
                _playerData.ProfitRatio, _playerData.DonutPerSecond);
            AddDonuts(donuts);
            _stats.AddEarnedWithOffline(donuts);
        }

        public void GiveAchievements(int numAchievements, bool hasAward, AwardsList award, double awardValue,
            double points)
        {
            if (_playerData.CompletedAchievements.Contains(numAchievements))
            {
                Debug.LogError($"<color=red>Trying to count achievements that are already counted! " +
                               $"Number Achievements: {numAchievements} </color>");
                return;
            }

            _playerData.CompletedAchievements.Add(numAchievements);

            if (!hasAward)
                return;

            switch (award)
            {
                case AwardsList.Donate:
                    AddDonate((int) awardValue);
                    break;
                case AwardsList.Donut:
                    AddDonuts(awardValue);
                    break;
                case AwardsList.Exp:
                    _levelManager.AddExp(awardValue);
                    break;
                case AwardsList.Level:
                    _levelManager.UpLevel();
                    break;
                case AwardsList.StrengthClick:
                    AddStrengthClick(awardValue);
                    break;
                default:
                    Debug.LogError($"<color=red>Error award : {award} </color>");
                    throw new ArgumentOutOfRangeException(nameof(award), award, null);
            }

            AddPointsAchievements(points);
            
            _notification.CreateNotification(
                $"{_local.TranslateWord("ACHIEVEMENTS_GIVE_ACHIEVEMENTS")} {award}({awardValue})");
        }

        public void AddPointsAchievements(double value) => _playerData.PointsAchievements += value;

        public void GiveQuests(int numQuests, QuestsAwards award, double awardValue)
        {
            if (_playerData.CompletedQuests.Contains(numQuests))
            {
                Debug.LogError($"<color=red>Trying to count achievements that are already counted! " +
                               $"Number quests: {numQuests} </color>");
                return;
            }

            _playerData.CompletedQuests.Add(numQuests);
            SetCurrentQuests(numQuests + 1);

            switch (award)
            {
                case QuestsAwards.Donut:
                    AddDonuts(awardValue);
                    break;
                case QuestsAwards.Donate:
                    AddDonate((int) awardValue);
                    break;
                case QuestsAwards.X2DonutPerSeconds:
                    SetX2DonutPerSeconds(true);
                    break;
                case QuestsAwards.X2ExpPerClick:
                    _levelManager.SetX2ExpForClick(true);
                    break;
                case QuestsAwards.X2DonutForClick:
                    SetX2Click(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(award), award, null);
            }
        }

        public void SetCurrentQuests(int numQuests) => _playerData.CurrentQuest = numQuests;
        public void SetNickName(string newName) => _playerData.NickName = newName;
        public void SetSteamId(long id) => _playerData.SteamID = id;

        private IEnumerator DonutPerSeconds()
        {
            yield return new WaitForSeconds(1);
            while (_playerData.DonutPerSecond > 0)
            {
                AddDonuts(_playerData.DonutPerSecond);
                _stats.AddEarnedWithDps(_playerData.DonutPerSecond);
                if (!_isCoroutineRun)
                    _isCoroutineRun = true;
                yield return new WaitForSeconds(1f);
            }

            _isCoroutineRun = false;
        }

        public bool AddDonuts(double value)
        {
            if (_playerData.Donut + value < 0 || double.IsInfinity(_playerData.Donut))
            {
                Debug.Log($"Unable to credit donuts,[{value}], there is [{_playerData.Donut}]");
                return false;
            }

            _playerData.Donut += value;
            _stats.AddEarnedDonuts(value);
            _mediator.UpdateDonutScore();
            return true;
        }

        public bool DelDonuts(double value)
        {
            if (_playerData.Donut - value < 0)
            {
                Debug.Log($"Not enough donuts, needs [{value}], there is [{_playerData.Donut}]");
                _notification.CreateNotification(_local.TranslateWord("NO_DONUTS"));
                return false;
            }

            _playerData.Donut -= Math.Round(value);
            _stats.AddSpentDonuts(Math.Round(value));
            Debug.Log($"Del donuts [{Math.Round(value)}], new donuts value [{_playerData.Donut}]");
            _mediator.UpdateDonutScore();
            return true;
        }

        public void SetDonutPerSeconds(double value)
        {
            var zero = _playerData.DonutPerSecond == 0;
            _playerData.DonutPerSecond = value;
            _mediator.UpdateDonutPerSeconds();
            if (zero) RestartCoroutineDps();
        }

        public void SetX2DonutPerSeconds(bool value) => _playerData.X2DonutPerSecond = value ? 2 : 1;


        public void AddDonate(int value)
        {
            _playerData.Donate += value;
            Debug.Log($"Add donate [{value}], new donate value [{_playerData.Donate}]");
            _mediator.UpdateDonateScore();
        }

        public bool DelDonate(int value)
        {
            if (_playerData.Donut - value < 0)
            {
                Debug.Log($"Not enough donuts, needs [{value}], there is [{_playerData.Donate}]");
                return false;
            }

            _playerData.Donate -= value;
            Debug.Log($"Del donate [{value}], new donate value [{_playerData.Donate}]");
            _mediator.UpdateDonateScore();
            return true;
        }


        public void AddStrengthClick(double value)
        {
            _playerData.StrengthClick += value;
            _mediator.UpdateDonutPerClick();
        }

        public void SetX2Click(bool value) => _playerData.X2DonutForClick = value ? 2 : 1;

        public void AddChanceCrit(int value)
        {
            if (_playerData.ChanceCrit >= 100 || _playerData.ChanceCrit + value >= 100)
                return;

            _playerData.ChanceCrit += value;
        }

        public void AddValueCrit() => _playerData.ValueCrit += 1;

        public void UpDonutLevel()
        {
            if (_playerData.DonutLevel >= _playerData.MaxDonutLevel)
                return;

            _playerData.DonutLevel++;
            _clickerSystem.LoadSpriteDonut();
            _mediator.UpdateDonutPerClick();
        }

        public void AddOfflineTime(int value)
        {
            if (_playerData.OfflineTime + value >= _playerData.MaxOfflineTime)
            {
                _playerData.OfflineTime = _playerData.MaxOfflineTime;
                return;
            }

            _playerData.OfflineTime += value;
        }

        public void AddOfflineProfitRatio(float value)
        {
            if (_playerData.ProfitRatio + value >= _playerData.MaxProfitRatio)
            {
                _playerData.ProfitRatio = _playerData.MaxProfitRatio;
                return;
            }

            _playerData.ProfitRatio += value;
        }
    }
}