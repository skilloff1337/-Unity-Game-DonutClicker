using _5._DataBase.Interfaces;
using _6._Audio.Interfaces;
using UnityEngine;
using Zenject;

namespace _6._Audio.Scripts
{
    public class AudioController : MonoBehaviour, IAudioController
    {
        [SerializeField] private AudioSource _switchButtonSound;
        [SerializeField] private AudioSource _backgroundMusic;
        [SerializeField] private AudioSource _clickSound;

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

        public void SetMusicVolume()
        {
            _backgroundMusic.volume = _settingsData.VolumeMusic;
        }

        public void SetSoundVolume()
        {
            _switchButtonSound.volume = _settingsData.VolumeSound;
            _clickSound.volume = _settingsData.VolumeSound;
        }
    }
}