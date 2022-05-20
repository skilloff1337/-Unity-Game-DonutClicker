using System;
using _0._Localization.Lists;
using _0._Localization.Scripts.Interfaces;
using _15._Notification.Scripts;
using _5._DataBase.Interfaces;
using _6._Audio.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _9._Settings.Scripts
{
    public class SettingsSystem : MonoBehaviour
    {
        [Header("Language")] 
        [SerializeField] private Sprite[] _imageLanguage;
        [SerializeField] private Image _imageLanguageSelect;

        [Header("Volume")] 
        [SerializeField] private TextMeshProUGUI _textSliderSoundVolume;
        [SerializeField] private TextMeshProUGUI _textSliderMusicVolume;
        [SerializeField] private Slider _sliderSoundVolume;
        [SerializeField] private Slider _sliderMusicVolume;

        [Header("NickName")] 
        [SerializeField] private TextMeshProUGUI _inputField;
        [SerializeField] private TextMeshProUGUI _textCurrentNick;

        private ILocalizationSystem _local;
        private ISettingsData _settingsData;
        private IAudioController _audioController;
        private IPlayerDataManager _playerManager;
        private INotificationSystem _notification;
        private IPlayerData _playerData;

        [Inject]
        private void Constructor(ILocalizationSystem localizationSystem, ISettingsData settingsData,
            IAudioController audioController, IPlayerDataManager playerManager, INotificationSystem notification,
            IPlayerData playerData)
        {
            _local = localizationSystem;
            _settingsData = settingsData;
            _audioController = audioController;
            _playerManager = playerManager;
            _notification = notification;
            _playerData = playerData;
        }

        private void Start()
        {
            LoadVolumeForSliders();
            UpdateCurrentFlagLanguage();
            _textCurrentNick.text = _playerData.NickName;
        }

        #region Language

        public void SwitchLanguage()
        {
            _settingsData.CurrentLanguages = _settingsData.CurrentLanguages switch
            {
                LanguagesType.Russian => LanguagesType.English,
                LanguagesType.English => LanguagesType.Russian,
                _ => throw new ArgumentOutOfRangeException()
            };

            UpdateCurrentFlagLanguage();
            _local.SetAutomaticText();
        }

        private void UpdateCurrentFlagLanguage()
        {
            _imageLanguageSelect.sprite = _settingsData.CurrentLanguages switch
            {
                LanguagesType.Russian => _imageLanguage[0],
                LanguagesType.English => _imageLanguage[1],
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        #endregion

        #region Volume

        public void SetVolumeSound()
        {
            var volume = _sliderSoundVolume.value <= 0.01 ? 0 : _sliderSoundVolume.value;
            _textSliderSoundVolume.text = volume <= 0.01 ? "0%" : $"{volume * 100:#,#}%";
            _settingsData.VolumeSound = volume;
            _audioController.SetSoundVolume();
        }

        public void SetVolumeMusic()
        {
            var volume = _sliderMusicVolume.value <= 0.01 ? 0 : _sliderMusicVolume.value;
            _textSliderMusicVolume.text = volume < 0.01 ? "0%" : $"{volume * 100:#,#}%";
            _settingsData.VolumeMusic = volume;
            _audioController.SetMusicVolume();
        }

        private void LoadVolumeForSliders()
        {
            var music = _settingsData.VolumeMusic;
            var sound = _settingsData.VolumeSound;
            _sliderMusicVolume.value = music;
            _sliderSoundVolume.value = sound;
            _textSliderMusicVolume.text = music < 0.01 ? "0%" : $"{music * 100:#,#}%";
            _textSliderSoundVolume.text = sound < 0.01 ? "0%" : $"{sound * 100:#,#}%";
        }

        #endregion

        #region NickName

        public void ChangeNickName()
        {
            var nick = _inputField.text;
            if (string.IsNullOrWhiteSpace(nick))
            {
                _notification.CreateNotification(_local.TranslateWord("SETTINGS_NICK_EMPTY"));
                return;
            }

            if (nick.Length is < 5 or > 31)
            {
                _notification.CreateNotification(_local.TranslateWord("SETTINGS_NICK_LENGTH"));
                _inputField.text = " ";
                return;
            }

            _playerManager.SetNickName(nick);
            _textCurrentNick.text = nick;
            _notification.CreateNotification($"{_local.TranslateWord("SETTINGS_NICK_CHANGE")} {nick}");
        }

        #endregion
    }
}