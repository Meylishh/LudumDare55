using System;
using UnityEngine;

namespace Script
{
    public class AudioManager: MonoBehaviour
    {
        public Sound[] musicSounds, sfxSounds;
        public AudioSource musicSource, sfxSource;

        public static AudioManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            PlayMusic("BackgroundMusic");
        }

        public void PlayMusic(string name)
        {
            var sound = Array.Find(musicSounds, x => x.Name == name);
            if (sound == null)
            {
                Debug.LogWarning("Sound Not Found");
            }
            else
            {
                musicSource.clip = sound.Clip;
                musicSource.Play();
            }
        } 
        public void PlaySFX(string name)
        {
            var sound = Array.Find(sfxSounds, x => x.Name == name);
            if (sound == null)
            {
                Debug.LogWarning("Sound Not Found");
            }
            else
            {
                sfxSource.PlayOneShot(sound.Clip);
            }
        }
        public void ToggleMusic()
        {
            musicSource.mute = !musicSource.mute;
        }
        public void ToggleSFX()
        {
            sfxSource.mute = !sfxSource.mute;
        }
        public void MusicVolume(float volume)
        {
            musicSource.volume = volume;
        }
        public void SFXVolume(float volume)
        {
            sfxSource.volume = volume;
        }
    }
}