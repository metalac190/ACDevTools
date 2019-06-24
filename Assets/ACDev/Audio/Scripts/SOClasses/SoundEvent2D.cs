using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACDev.Audio
{
    [CreateAssetMenu(menuName = "ACDev/Audio/SoundEvent2D", fileName = "NewSoundEvent2D")]
    public class SoundEvent2D : ScriptableObject
    {
        [Header("Base Settings")]
        [SerializeField] AudioClip[] _clipVariations;
        public AudioClip Clip { get; private set; }
        [Range(0, 1)]
        [SerializeField] float _minVolume = 1f;
        [Range(0, 1)]
        [SerializeField] float _maxVolume = 1f;
        public float Volume { get; private set; }
        [Range(.5f, 1.5f)]
        [SerializeField] float _minPitch = 1f;
        [Range(.5f, 1.5f)]
        [SerializeField] float _maxPitch = 1f;
        public float Pitch { get; private set; }

        public void Play()
        {
            if (_clipVariations.Length == 0) return;

            Clip = _clipVariations[Random.Range(0, _clipVariations.Length)];
            Volume = Random.Range(_minVolume, _maxVolume);
            Pitch = Random.Range(_minPitch, _maxPitch);

            AudioManager.Instance.PlaySound2D(this);
        }
    }
}


