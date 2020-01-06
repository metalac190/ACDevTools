using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // use 2 music sources so that we can do cross blending
    [SerializeField] AudioSource _musicSource1 = null;
    [SerializeField] AudioSource _musicSource2 = null;

    private bool _music1SourcePlaying = false;
    [SerializeField] float _currentMusicVolume = 1;
    public float CurrentMusicVolume
    {
        get { return _currentMusicVolume; }
        private set
        {
            value = Mathf.Clamp(value, 0, 1);
        }
    }

    private void Start()
    {
        _musicSource1.volume = CurrentMusicVolume;
        _musicSource2.volume = CurrentMusicVolume;
    }

    #region Get/Set
    AudioSource GetActiveMusicSource()
    {
        AudioSource activeSource = (_music1SourcePlaying) ? _musicSource1 : _musicSource2;
        return activeSource;
    }
    public void SetMusicVolume(float newVolume)
    {
        CurrentMusicVolume = newVolume;
        GetActiveMusicSource().volume = CurrentMusicVolume;
    }
    public void SetMusicVolumeWithBlend(float targetVolume, float volumeBlendDuration)
    {
        AudioSource activeSource = GetActiveMusicSource();
        StartCoroutine(UpdateMusicVolume(activeSource, targetVolume, volumeBlendDuration));
    }
    IEnumerator UpdateMusicVolume(AudioSource activeSource, float targetVolume, float fadeTime)
    {
        float startingVolume = CurrentMusicVolume;
        // fade volume
        for (float t = 0; t <= fadeTime; t += Time.deltaTime)
        {
            float newVolume = Mathf.Lerp(startingVolume, targetVolume, t / fadeTime);
            activeSource.volume = newVolume;
            yield return null;
        }
        CurrentMusicVolume = targetVolume;
    }
    #endregion

    #region Play Music
    public void PlayMusic(AudioClip musicClip)
    {
        // determine which source is active
        AudioSource activeSource = GetActiveMusicSource();

        activeSource.clip = musicClip;
        activeSource.volume = CurrentMusicVolume;
        activeSource.Play();
    }
    public void PlayMusicWithFade(AudioClip musicClip, float transitionDuration)
    {
        AudioSource activeSource = GetActiveMusicSource();
        StartCoroutine(UpdateMusicWithFade(activeSource, musicClip, transitionDuration));
    }
    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip musicClip, float transitionDuration)
    {
        // validate source
        if (activeSource.isPlaying == false)
        {
            activeSource.Play();
        }
        // fade out
        float startingVolume = CurrentMusicVolume;
        float fadeOutTransitionDuration = transitionDuration / 2;
        for (float t = 0; t <= fadeOutTransitionDuration; t += Time.deltaTime)
        {
            activeSource.volume = Mathf.Lerp(startingVolume, 0, t / fadeOutTransitionDuration);
            yield return null;
        }
        // start new music track
        activeSource.Stop();
        activeSource.clip = musicClip;
        activeSource.Play();
        // fade in
        float fadeInTransitionDuration = transitionDuration / 2;
        for (float t = 0; t <= fadeInTransitionDuration; t += Time.deltaTime)
        {
            activeSource.volume = Mathf.Lerp(0, CurrentMusicVolume, t / fadeInTransitionDuration);
            yield return null;
        }
    }
    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime)
    {
        AudioSource activeSource = GetActiveMusicSource();
        AudioSource newSource = (_music1SourcePlaying) ? _musicSource2 : _musicSource1;

        // swap the source
        _music1SourcePlaying = !_music1SourcePlaying;

        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
    }
    private IEnumerator UpdateMusicWithCrossFade
        (AudioSource originalSource, AudioSource newSource, float transitionDuration)
    {
        float startingVolume = CurrentMusicVolume;
        for (float t = 0.0f; t <= transitionDuration; t += Time.deltaTime)
        {
            originalSource.volume = Mathf.Lerp(CurrentMusicVolume, 0, t / transitionDuration);
            newSource.volume = Mathf.Lerp(0, CurrentMusicVolume, t / transitionDuration);
            yield return null;
        }

        originalSource.Stop();
    }
    #endregion
}

