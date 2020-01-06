using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MusicPlayer))]
[RequireComponent(typeof(SoundPlayer))]
public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get { return _instance; }
        private set { _instance = value; }
    }

    private MusicPlayer _musicPlayer;
    private SoundPlayer _soundPlayer;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            Init();
        }  
    }

    void Init()
    {
        _musicPlayer = GetComponent<MusicPlayer>();
        _soundPlayer = GetComponent<SoundPlayer>();
    }

    #region Music
    public void SetMusicVolume(float newVolume)
    {
        _musicPlayer.SetMusicVolume(newVolume);
    }
    public void SetMusicVolume(float newVolume, float volumeBlendDuration)
    {
        _musicPlayer.SetMusicVolumeWithBlend(newVolume, volumeBlendDuration);
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if(musicClip == null) { return; }

        _musicPlayer.PlayMusic(musicClip);
    }
    public void PlayMusicWithFade(AudioClip musicClip, float transitionDuration = 2.5f)
    {
        if(musicClip == null) { return; }

        _musicPlayer.PlayMusicWithFade(musicClip, transitionDuration);
    }
    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionDuration = 2.5f)
    {
        if(musicClip == null) { return; }

        _musicPlayer.PlayMusicWithCrossFade(musicClip, transitionDuration);
    }
    #endregion

    #region Sounds
    public void PlaySound2D(AudioClip clip, float volume)
    {
        if(clip == null) { return; }

        _soundPlayer.PlaySound2D(clip, volume);
    }
    public void PlaySound2D(AudioClip clip, float volume, float pitch)
    {
        if(clip == null) { return; }

        _soundPlayer.PlaySound2D(clip, volume, pitch);
    }
    public void PlaySound2D(SoundEvent sound)
    {
        if(sound == null) { return; }

        _soundPlayer.PlaySound2D(sound);
    }

    public void PlaySound3D(AudioClip clip, float volume, Vector3 position)
    {
        if(clip == null) { return; }

        _soundPlayer.PlaySound3D(clip, volume, position);
    }
    public void PlaySound3D(SoundEvent sound, Vector3 position)
    {
        if(sound == null) { return; }

        _soundPlayer.PlaySound3D(sound, position);
    }
    #endregion
}

