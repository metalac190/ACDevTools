using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACDev.Audio
{
    [CreateAssetMenu(menuName = "ACDev/Audio/SoundEvent3D", fileName = "NewSoundEvent3D")]
    public class SoundEvent3D : ScriptableObject
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

        public void Play(Vector3 position)
        {
            if (_clipVariations.Length == 0) return;

            Clip = _clipVariations[Random.Range(0, _clipVariations.Length)];
            Volume = Random.Range(_minVolume, _maxVolume);
            Pitch = Random.Range(_minPitch, _maxPitch);

            AudioManager.Instance.PlaySound3D(this, position);
        }
    }
}
