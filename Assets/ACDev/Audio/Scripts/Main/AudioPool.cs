using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : MonoBehaviour
{
    [SerializeField] AudioSource _poolSource = null;
    [SerializeField] int _initialPoolSize = 5;
    private Queue<AudioSource> sourcePool = new Queue<AudioSource>();

    private void Start()
    {
        CreateInitialPool(_initialPoolSize);
    }

    void CreateInitialPool(int startingPoolSize)
    {
        for (int i = 0; i < startingPoolSize; i++)
        {
            CreatePoolObject();
        }
    }

    public AudioSource Get()
    {
        if (sourcePool.Count == 0)
        {
            CreatePoolObject();
        }

        AudioSource objectFromPool = sourcePool.Dequeue();
        ResetPoolObject(objectFromPool);
        return objectFromPool;
    }

    public void Return(AudioSource sourceToReturn)
    {
        sourceToReturn.gameObject.SetActive(false);
        sourcePool.Enqueue(sourceToReturn);
    }

    private void CreatePoolObject()
    {
        AudioSource newSource = Instantiate(_poolSource, gameObject.transform);
        newSource.gameObject.SetActive(false);
        sourcePool.Enqueue(newSource);
    }

    private void ResetPoolObject(AudioSource poolSource)
    {
        poolSource.clip = null;
        poolSource.loop = false;
        poolSource.playOnAwake = false;
        poolSource.volume = _poolSource.volume;
        poolSource.pitch = _poolSource.pitch;
        poolSource.minDistance = _poolSource.minDistance;
        poolSource.maxDistance = _poolSource.maxDistance;
    }
}

