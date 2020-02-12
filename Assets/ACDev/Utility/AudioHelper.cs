using UnityEngine;

public static class AudioHelper
{
    public static AudioSource PlayClip2D(AudioClip clip, float volume)
    {
        // create
        GameObject audioObject = new GameObject("2DAudio");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        //configure
        audioSource.clip = clip;
        audioSource.volume = volume;
        // activate
        audioSource.Play();
        Object.Destroy(audioObject, clip.length);
        // return in case the caller wants to do other things
        return audioSource;
    }

    public static AudioSource PlayClip3D(AudioClip clip, float volume, Vector3 position)
    {
        // create
        GameObject audioObject = new GameObject("3DAudio");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        //configure
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.spatialBlend = 1;
        audioObject.transform.position = position;
        // activate
        audioSource.Play();
        Object.Destroy(audioObject, clip.length);
        // return in case the caller wants to do other things
        return audioSource;
    }
}
