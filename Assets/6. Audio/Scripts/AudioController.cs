using System;
using _5._DataBase.Interfaces;
using UnityEngine;
using Zenject;

namespace _6._Audio.Scripts
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource switchButtonSound;
        [SerializeField] private AudioSource backgroundMusic;
        [SerializeField] private AudioSource clickSound;

        private ISettingsData _settingsData;

        [Inject]
        private void Constructor(ISettingsData settingsData)
        {
            _settingsData = settingsData;
        }

        private void Start()
        {
            SetAllVolume();
        }

        private void SetAllVolume()
        {
            SetMusicVolume();
            SetSoundVolume();
        }

        private void SetMusicVolume()
        {
            backgroundMusic.volume = _settingsData.VolumeMusic;
        }

        private void SetSoundVolume()
        {
            switchButtonSound.volume = _settingsData.VolumeSound;
            clickSound.volume = _settingsData.VolumeSound;
        }
    }
}