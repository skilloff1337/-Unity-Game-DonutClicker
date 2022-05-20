using System;
using _11._Shop.Data;
using _5._DataBase.Data;
using _5._DataBase.Interfaces;
using UnityEngine;
using Zenject;

namespace _5._DataBase.Scripts
{
    public class StatisticsDataManager : MonoBehaviour, IStatisticsDataManager
    {
        private IPlayerData _playerData;
        private StatisticsData _stats;

        private DateTime _statsTime;

        [Inject]
        private void Constructor(IPlayerData playerData)
        {
            _playerData = playerData;
            _stats = playerData.StatisticsData;
            _statsTime = DateTime.Now;
        }

        public void UpdateStatistics()
        {
            AddPlayedTime();
            SetLongestSession();
            SetMaxClicksPerSession();
            SetTotalDamage();
            SetTotalDamageDonut();
            SetTotalDamageLevel();
            SetTotalDamageUpgrade();
        }

        public void UpdateStatisticsOnExit()
        {
            AddPlayedTime();
        }

        public void AllUpdateStatistics()
        {
            UpdateStatistics();
            UpdateStatisticsOnExit();
        }

        public void AddClicks() => _stats.Clicks++;
        public void AddClicksCurrentSession() => _stats.ClicksCurrentSession++;

        public void SetMaxClicksPerSession()
        {
            if (_stats.ClicksCurrentSession > _stats.MaxClicksPerSession)
                _stats.MaxClicksPerSession = _stats.ClicksCurrentSession;
        }

        public void AddBansForAntiClicker() => _stats.BansForAntiClicker++;
        public void AddGameLogins() => _stats.GameLogins++;
        public void AddViewsAds() => _stats.ViewsAds++;

        public void SetCurrentPositionInLeadersBoard(double value)
        {
            _stats.CurrentPositionInLeadersBoard = value;
            SetMaxPositionInLeadersBoard();
        }

        private void SetMaxPositionInLeadersBoard()
        {
            if (_stats.CurrentPositionInLeadersBoard > _stats.MaxPositionInLeadersBoard)
                _stats.MaxPositionInLeadersBoard = _stats.CurrentPositionInLeadersBoard;
        }

        public void AddEarnedExp(double value) => _stats.EarnedExp += value;

        public void SetTotalDamage()
        {
            var damage = (_playerData.StrengthClick + _playerData.LevelData.LevelMultipleClick) *
                         _playerData.X2DonutForClick *
                         _playerData.DonutLevel;
            _stats.TotalDamage = damage;
        }

        public void SetTotalDamageLevel() => _stats.TotalDamageLevel = _playerData.LevelData.LevelMultipleClick;

        public void SetTotalDamageUpgrade() => _stats.TotalDamageLevel = _playerData.LevelData.LevelMultipleClick;

        public void SetTotalDamageDonut() => _stats.TotalDamageLevel = _playerData.DonutLevel;

        public void AddBuyItemsInShop() => _stats.BuyItemsInShop++;

        public void AddBuyUpgradesInShop() => _stats.BuyUpgradesInShop++;

        public void AddBuyDonateInShop() => _stats.BuyDonateInShop++;
        public void AddBuyConcreteItemInShop(int value)
        {
            switch ((ItemType)value)
            {
                case ItemType.FormForBaking:
                    _stats.BuyFormForBaking++;
                    break;
                case ItemType.Baker:
                    _stats.BuyBaker++;
                    break;
                case ItemType.Furnace:
                    _stats.BuyFurnace++;
                    break;
                case ItemType.ConfectioneryStall:
                    _stats.BuyConfectioneryStall++;
                    break;
                case ItemType.ConfectioneryShop:
                    _stats.BuyConfectioneryShop++;
                    break;
                case ItemType.Bakery:
                    _stats.BuyBakery++;
                    break;
                case ItemType.Factory:
                    _stats.BuyFactory++;
                    break;
                case ItemType.MolecularKitchen:
                    _stats.BuyMolecularKitchen++;
                    break;
                case ItemType.Laboratory:
                    _stats.BuyLaboratory++;
                    break;
                case ItemType.Station:
                    _stats.BuyStation++;
                    break;
                case ItemType.DonutHole:
                    _stats.BuyDonutHole++;
                    break;
                case ItemType.Source:
                    _stats.BuySource++;
                    break;
                case ItemType.Count:
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public void AddEarnedDonuts(double value) => _stats.EarnedDonuts += value;

        public void AddEarnedWithClicks(double value) => _stats.EarnedWithClicks += value;

        public void AddEarnedWithAds(double value) => _stats.EarnedWithAds += value;

        public void AddEarnedWithDps(double value) => _stats.EarnedWithDps += value;

        public void AddEarnedWithDonate(double value) => _stats.EarnedWithDonate += value;

        public void AddEarnedWithOffline(double value) => _stats.EarnedWithOffline  += value;

        public void AddEarnedDonate(double value) => _stats.EarnedWithOffline += value;
        public void AddSpentDonuts(double value) => _stats.SpentDonuts += value;

        public void AddSpentWithShop(double value) => _stats.SpentWithShop += value;

        public void AddSpentWithUpgrade(double value) => _stats.SpentWithUpgrade += value;

        public void AddSpentDonate(double value) => _stats.SpentDonate += value;

        public void AddPlayedTime()
        {
            var timeSpan = DateTime.Now - _statsTime;
            _statsTime = DateTime.Now;
            _stats.PlayedTime.Days += timeSpan.Days;
            _stats.PlayedTime.Hours += timeSpan.Hours;
            _stats.PlayedTime.Minutes += timeSpan.Minutes;
            _stats.PlayedTime.Seconds += timeSpan.Seconds;

            if (_stats.PlayedTime.Seconds >= 60)
            {
                _stats.PlayedTime.Minutes += Math.DivRem(_stats.PlayedTime.Seconds, 60, out var rest);
                _stats.PlayedTime.Seconds = rest;
            }
            if (_stats.PlayedTime.Minutes >= 60)
            {
                _stats.PlayedTime.Hours += Math.DivRem(_stats.PlayedTime.Seconds, 60, out var rest);
                _stats.PlayedTime.Minutes = rest;
            }
            if (_stats.PlayedTime.Hours >= 24)
            {
                _stats.PlayedTime.Days += Math.DivRem(_stats.PlayedTime.Seconds, 60, out var rest);
                _stats.PlayedTime.Hours = rest;
            }
        }

        public void SetLongestSession()
        {
            var timeSpan = DateTime.Now - _stats.EnterLogin;
            var longestSession = (_stats.LongestSession.Days * 86400) + (_stats.LongestSession.Hours * 3600) +
                                 (_stats.LongestSession.Minutes * 60) + _stats.LongestSession.Seconds;
            if (!(timeSpan.TotalSeconds > longestSession)) 
                return;
            
            _stats.LongestSession.Days = timeSpan.Days;
            _stats.LongestSession.Hours = timeSpan.Hours;
            _stats.LongestSession.Minutes = timeSpan.Minutes;
            _stats.LongestSession.Seconds = timeSpan.Seconds;
        }
    }
}