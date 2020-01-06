using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] AudioPool _sound2DPool = null;
    [SerializeField] AudioPool _sound3DPool = null;

    #region Play Sounds
    public void PlaySound2D(AudioClip clip, float volume)
    {
        AudioSource newSource = _sound2DPool.Get();
        // setup
        newSource.clip = clip;
        newSource.volume = volume;

        ActivatePooledSound2D(newSource);
    }
    public void PlaySound2D(AudioClip clip, float volume, float pitch)
    {
        AudioSource newSource = _sound2DPool.Get();
        // setup
        newSource.clip = clip;
        newSource.volume = volume;
        newSource.pitch = pitch;

        ActivatePooledSound2D(newSource);
    }
    public void PlaySound2D(SoundEvent sound2D)
    {
        AudioSource newSource = _sound2DPool.Get();
        // setup
        newSource.clip = sound2D.Clip;
        newSource.volume = sound2D.Volume;
        newSource.pitch = sound2D.Pitch;

        ActivatePooledSound2D(newSource);
    }

    public void PlaySound3D(AudioClip clip, float volume, Vector3 position)
    {
        AudioSource newSource = _sound3DPool.Get();
        // setup
        newSource.clip = clip;
        newSource.volume = volume;

        ActivatePooledSound3D(newSource, position);
    }
    public void PlaySound3D(SoundEvent sound3D, Vector3 position)
    {
        AudioSource newSource = _sound2DPool.Get();
        // setup
        newSource.clip = sound3D.Clip;
        newSource.volume = sound3D.Volume;
        newSource.pitch = sound3D.Pitch;

        ActivatePooledSound3D(newSource, position);
    }

    private void ActivatePooledSound2D(AudioSource newSource)
    {
        newSource.gameObject.SetActive(true);
        newSource.Play();
        StartCoroutine(DisableSoundAfterFinished(newSource));
    }
    private void ActivatePooledSound3D(AudioSource newSource, Vector3 position)
    {
        newSource.gameObject.transform.position = position;

        newSource.gameObject.SetActive(true);
        newSource.Play();
        StartCoroutine(DisableSoundAfterFinished(newSource));
    }
    IEnumerator DisableSoundAfterFinished(AudioSource sourceToDisable)
    {
        if (sourceToDisable.clip == null)
        {
            Debug.LogWarning("AudioPlayer cannot disable sound, no clip specified!");
            yield break;
        }
        // ensure that looping isn't false. We don't want to disable a looping sound
        sourceToDisable.loop = false;

        float clipDuration = sourceToDisable.clip.length;
        yield return new WaitForSeconds(clipDuration);
        // disable
        sourceToDisable.Stop();
        _sound2DPool.Return(sourceToDisable);
    }
    #endregion
}


