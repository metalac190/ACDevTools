﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by: Adam Chandler, using tutorials from Unity3D.college
/// This script allows you to create your own object pool scripts from this template.
/// To use:
/// 1. Inherit from this class to create any type of Object pool you want.
/// 2. Make sure to clean up your objects before sending them back to the pool. This script
/// 3. Optionally, override ResetDefaults() to do extra work on object before returning to pool
/// does not return object default settings: that is left to the user.
/// </summary>
public abstract class MBObjectPool<T> : MonoBehaviour where T : Component
{

    [Header("Pool Settings")]
    [SerializeField] private T _prefab = null;
    [SerializeField] private int _startingPoolSize = 5;

    protected Queue<T> _objectPool = new Queue<T>();

    #region Initialization
    private void Awake()
    {
        CheckReferences();
        CreateInitialPool(_startingPoolSize);
    }
    #endregion

    #region Public
    /// <summary>
    /// Retrieve a deactivated object from the pool and Activate it
    /// </summary>
    /// <returns></returns>
    public T GetObject()
    {
        if (_objectPool.Count == 0)
        {
            CreateNewPoolObject();
        }

        T newPoolObject = _objectPool.Dequeue();
        newPoolObject.gameObject.SetActive(true);

        return newPoolObject;
    }
    /// <summary>
    /// Disables an object and returns it to the pool. Make sure you return object
    /// to default settings if you've made any changes to the object's attributes.
    /// </summary>
    /// <param name="objectToReturn"></param>
    public void ReturnObject(T objectToReturn)
    {
        ResetObjectDefaults(objectToReturn);
        objectToReturn.gameObject.SetActive(false);
        _objectPool.Enqueue(objectToReturn);
    }
    #endregion

    /// <summary>
    /// Override this method to do additional, specific work on the object to reset
    /// its defaults before returning to the pol
    /// </summary>
    /// <param name="pooledObject"></param>
    protected virtual void ResetObjectDefaults(T pooledObject)
    {

    }

    private void CheckReferences()
    {
        if (_prefab == null)
        {
            Debug.LogError(this + ": no pool prefab defined");
            this.enabled = false;
            return;
        }
    }

    private void CreateInitialPool(int startingPoolSize)
    {
        for (int i = 0; i < startingPoolSize; i++)
        {
            CreateNewPoolObject();
        }
    }

    T CreateNewPoolObject()
    {
        T newObject = Instantiate(_prefab);

        newObject.transform.SetParent(this.transform);
        newObject.gameObject.name = _prefab.gameObject.name;
        newObject.gameObject.SetActive(false);
        _objectPool.Enqueue(newObject);

        return newObject;
    }
}

