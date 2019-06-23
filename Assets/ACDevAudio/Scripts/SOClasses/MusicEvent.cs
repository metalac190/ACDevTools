using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACDev.Audio
{
    [CreateAssetMenu(menuName = "ACDev/Audio/MusicEvent", fileName = "NewMusicEvent")]
    public class MusicEvent : ScriptableObject
    {
        [Header("Base Settings")]
        [SerializeField] AudioClip _musicClip;
        [SerializeField] bool _crossFade = false;
        [SerializeField] float _fadeTime = 0;

        public void Play()
        {
            if (_musicClip == null) { return; }
            
            // if no fade, don't worry about it
            if(_fadeTime <= 0)
            {
                AudioPlayer.Instance.PlayMusic(_musicClip);
            }
            // add a fade
            else
            {
                if(_crossFade == true)
                {
                    AudioPlayer.Instance.PlayMusicWithFade(_musicClip, _fadeTime);
                }
                else
                {
                    AudioPlayer.Instance.PlayMusicWithCrossFade(_musicClip, _fadeTime);
                }
            }
        }
    }
}

