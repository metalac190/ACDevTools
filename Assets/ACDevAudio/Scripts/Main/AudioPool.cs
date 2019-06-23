using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : MonoBehaviour
{
    [SerializeField] AudioSource poolSource;
    private Queue<AudioSource> sourcePool = new Queue<AudioSource>();

    public AudioSource Get()
    {
        if(sourcePool.Count == 0)
        {
            AddSourceToPool();
        }

        return sourcePool.Dequeue();
    }

    public void Return(AudioSource sourceToReturn)
    {
        sourceToReturn.gameObject.SetActive(false);
        sourcePool.Enqueue(sourceToReturn);
    }

    private void AddSourceToPool()
    {
        //TODO spawn a new Audio Source properly (similar to the Create Audio Source button in Editor)
        AudioSource newSource = new AudioSource();
        newSource = GameObject.Instantiate(newSource);
        newSource.gameObject.SetActive(false);
        sourcePool.Enqueue(newSource);
    }
}
