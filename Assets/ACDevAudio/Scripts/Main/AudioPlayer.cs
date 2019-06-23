using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACDev.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        #region Singleton
        private static bool _shuttingDown = false;
        private static object _lock = new Object();
        private static AudioPlayer _instance;

        public static AudioPlayer Instance
        {
            get
            {
                if (_shuttingDown)
                {
                    Debug.LogWarning("[Singleton Instance '" + typeof(AudioPlayer) 
                        + "' already destroyed. Returning null.");
                    return null;
                }

                lock (_lock)
                {
                    if(_instance == null)
                    {
                        // search for existing instance first
                        _instance = FindObjectOfType<AudioPlayer>();
                        // it doesn't exist in scene, so create it
                        if(_instance == null)
                        {
                            CreatePersistentAudioPlayer();
                        }
                    }

                    return _instance;
                }
            }
        }

        private static void CreatePersistentAudioPlayer()
        {
            GameObject singletonObject = new GameObject();
            _instance = singletonObject.AddComponent<AudioPlayer>();
            singletonObject.name = "_AudioPlayer";

            // make it persistent
            DontDestroyOnLoad(singletonObject);
        }

        private void OnApplicationQuit()
        {
            _shuttingDown = true;
        }

        private void OnDestroy()
        {
            _shuttingDown = true;
        }
        #endregion

        private AudioSource _musicSource1;
        private AudioSource _musicSource2;
        private AudioSource _sfxSource;
        private float _musicVolume = 1;

        private bool _music1SourcePlaying = false;

        #region Initialization
        private void Awake()
        {
            AudioPlayerSetup();
        }

        private void AudioPlayerSetup()
        {
            _musicSource1 = this.gameObject.AddComponent<AudioSource>();
            _musicSource2 = this.gameObject.AddComponent<AudioSource>();
            _sfxSource = this.gameObject.AddComponent<AudioSource>();

            _musicSource1.loop = true;
            _musicSource2.loop = true;
        }
        #endregion

        #region Music
        AudioSource GetActiveMusicSource()
        {
            AudioSource activeSource = (_music1SourcePlaying) ? _musicSource1 : _musicSource2;
            return activeSource;
        }
        
        public void SetMusicVolume(float newVolume)
        {
            newVolume = Mathf.Clamp(newVolume, 0, 1);

            _musicSource1.volume = newVolume;
            _musicSource2.volume = newVolume;
        }
        public void FadeMusicVolume(float targetVolume, float fadeTime)
        {
            AudioSource activeSource = GetActiveMusicSource();
            StartCoroutine(UpdateMusicVolume(activeSource, targetVolume, fadeTime));
        }
        IEnumerator UpdateMusicVolume(AudioSource activeSource, float targetVolume, float fadeTime)
        {
            float startingVolume = activeSource.volume;
            // fade volume
            for (float t = 0; t <= fadeTime; t+= Time.deltaTime)
            {
                float newVolume = Mathf.Lerp(startingVolume, targetVolume, t / fadeTime);
                activeSource.volume = newVolume;
                yield return null;
            }
        }

        public void PlayMusic(AudioClip musicClip)
        {
            // determine which source is active
            AudioSource activeSource = GetActiveMusicSource();

            activeSource.clip = musicClip;
            activeSource.volume = 1;
            activeSource.Play();
        }
        public void PlayMusicWithFade(AudioClip musicClip, float transitionTime = 1.0f)
        {
            AudioSource activeSource = GetActiveMusicSource();
            StartCoroutine(UpdateMusicWithFade(activeSource, musicClip, transitionTime));
        }
        private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip musicClip, float transitionTime)
        {
            // validate source
            if(activeSource.isPlaying == false)
            {
                activeSource.Play();
            }
            // fade out
            for (float t = 0; t <= transitionTime; t += Time.deltaTime)
            {
                activeSource.volume = (1 - (t / transitionTime));
                yield return null;
            }

            activeSource.Stop();
            activeSource.clip = musicClip;
            activeSource.Play();

            // fade in
            for (float t = 0; t < transitionTime; t += Time.deltaTime)
            {
                activeSource.volume = (t / transitionTime);
                yield return null;
            }
        }
        public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)
        {
            AudioSource activeSource = GetActiveMusicSource();
            AudioSource newSource = (_music1SourcePlaying) ? _musicSource1 : _musicSource2;

            // swap the source
            _music1SourcePlaying = !_music1SourcePlaying;

            newSource.clip = musicClip;
            newSource.Play();
            StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
        }
        private IEnumerator UpdateMusicWithCrossFade(AudioSource originalSource, AudioSource newSource, float transitionTime)
        {
            for (float t = 0.0f; t <= transitionTime; t += Time.deltaTime)
            {
                originalSource.volume = (1 - (t / transitionTime));
                newSource.volume = (t / transitionTime);
                yield return null;
            }

            originalSource.Stop();
        }

        #endregion

        //TODO make these use audioSource pooling
        #region Simple Sounds
        public void PlaySound2D(AudioClip clip, float volume)
        {
            _sfxSource.volume = volume;
            _sfxSource.PlayOneShot(clip);
        }
        public void PlaySound2D(AudioClip clip, float volume, float pitch)
        {
            _sfxSource.volume = volume;
            _sfxSource.pitch = pitch;
            _sfxSource.PlayOneShot(clip);
        }
        public void PlaySound2D(AudioClip clip, float volume, float pitch, float minAttenuation, float maxAttenuation)
        {

        }

        public void PlaySound3D(AudioClip clip, float volume, Vector3 position)
        {
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }
        public void PlaySound3D(AudioClip clip, float volume, float pitch, Vector3 position)
        {
            //TODO incorporate pitch
            Debug.LogWarning("PlaySound3D pitch not yet functional.");
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }
        #endregion
    }
}

