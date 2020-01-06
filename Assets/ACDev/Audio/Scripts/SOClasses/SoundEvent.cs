using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ACDev/Audio/SoundEvent", fileName = "NewSoundEvent")]
public class SoundEvent : ScriptableObject
{
    [Header("Base Settings")]
    [SerializeField] AudioClip[] _clipVariations = new AudioClip[0];
    [Range(0, 1)]
    [SerializeField] float _minVolume = 1f;
    [Range(0, 1)]
    [SerializeField] float _maxVolume = 1f;
    [Range(.5f, 1.5f)]
    [SerializeField] float _minPitch = 1f;
    [Range(.5f, 1.5f)]
    [SerializeField] float _maxPitch = 1f;

    public AudioClip Clip { get; private set; }
    public float Volume { get; private set; }
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
        get { return _maxAttenuation; }
        private set
        {
            if (value < MinAttenuation)
            {
                value = MinAttenuation;
            }
            _minAttenuation = value;
        }
    }

    public void Play2D()
    {
        if (_clipVariations.Length == 0) return;

        SetVariationValues();
        AudioManager.Instance.PlaySound2D(this);
    }

    public void Play3D(Vector3 position)
    {
        if (_clipVariations.Length == 0) return;

        SetVariationValues();
        AudioManager.Instance.PlaySound3D(this, position);
    }

    private void SetVariationValues()
    {
        Clip = _clipVariations[Random.Range(0, _clipVariations.Length)];
        Volume = Random.Range(_minVolume, _maxVolume);
        Pitch = Random.Range(_minPitch, _maxPitch);
    }
}
