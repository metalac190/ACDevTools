using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACDev.Audio
{
    [CreateAssetMenu(menuName = "ACDev/Audio/SoundEvent3D", fileName = "NewSoundEvent3D")]
    public class SoundEvent3D : SoundEvent
    {
        [Header("3D Settings")]
        [SerializeField] float _minAttenuation = 1;
        public float MinAttenuation
        {
            get { return _minAttenuation; }
            private set
            {
                value = Mathf.Clamp(value, 0, MaxAttenuation);
                _minAttenuation = value;
            }
        }
        [SerializeField] float _maxAttenuation = 500;
        public float MaxAttenuation
        {
            get { return _minAttenuation; }
            private set
            {
                if(value < MinAttenuation)
                {
                    value = MinAttenuation;
                }
                _minAttenuation = value;
            }
        }

        public override void Play(AudioClip clip, float volume, float pitch)
        {
            AudioPlayer.Instance.PlaySound2D(clip, volume, pitch, MinAttenuation, MaxAttenuation);
        }
    }
}
