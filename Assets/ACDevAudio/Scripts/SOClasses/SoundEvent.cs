using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACDev.Audio
{
    public abstract class SoundEvent : ScriptableObject
    {
        public abstract void Play(AudioClip clip, float volume, float pitch);

        [Header("Base Settings")]
        [SerializeField] AudioClip[] _clipVariations;
        [Range(0, 1)]
        [SerializeField] float _minVolume = 1f;
        public float MinVolume
        {
            get { return _minVolume; }
            private set
            {
                value = Mathf.Clamp(value, 0, 1);
                _minVolume = value;
            }
        }
        [Range(0, 1)]
        [SerializeField] float _maxVolume = 1f;
        public float MaxVolume
        {
            get { return _maxVolume; }
            private set
            {
                value = Mathf.Clamp(value, 0, 1);
                _maxVolume = value;
            }
        }
        [Range(.5f, 1.5f)]
        [SerializeField] float _minPitch = 1f;
        public float MinPitch
        {
            get { return _minPitch; }
            private set
            {
                value = Mathf.Clamp(value, .5f, 1.5f);
                _minPitch = value;
            }
        }
        [Range(.5f, 1.5f)]
        [SerializeField] float _maxPitch = 1f;
        public float MaxPitch
        {
            get { return _maxPitch; }
            private set
            {
                value = Mathf.Clamp(value, .5f, 1.5f);
            }
        }
        
        public void Play()
        {
            if (_clipVariations.Length == 0) return;

            AudioClip clip = _clipVariations[Random.Range(0, _clipVariations.Length)];
            float volume = Random.Range(_minVolume, _maxVolume);
            float pitch = Random.Range(_minPitch, _maxPitch);

            Play(clip, volume, pitch);
        }
    }
}

