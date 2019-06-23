using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACDev.Audio
{
    [CreateAssetMenu(menuName = "ACDev/Audio/SoundEvent2D", fileName = "NewSoundEvent2D")]
    public class SoundEvent2D : SoundEvent
    {
        //[Header("2D Settings")]

        public override void Play(AudioClip clip, float volume, float pitch)
        {
            AudioPlayer.Instance.PlaySound2D(clip, volume, pitch);
        }
    }
}


