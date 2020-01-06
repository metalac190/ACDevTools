using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ACDev/Audio/MusicEvent", fileName = "NewMusicEvent")]
public class MusicEvent : ScriptableObject
{
    [Header("Base Settings")]
    [SerializeField] AudioClip _musicClip = null;
    [SerializeField] bool _crossFade = false;
    [SerializeField] float _fadeTime = 0;

    public void Play()
    {
        if (_musicClip == null) { return; }
            
        // if no fade, don't worry about it
        if(_fadeTime <= 0)
        {
            AudioManager.Instance.PlayMusic(_musicClip);
        }
        // add a fade
        else
        {
            if(_crossFade == true)
            {
                AudioManager.Instance.PlayMusicWithFade(_musicClip, _fadeTime);
            }
            else
            {
                AudioManager.Instance.PlayMusicWithCrossFade(_musicClip, _fadeTime);
            }
        }
    }
}

