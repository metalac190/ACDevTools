using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by: Adam Chandler, using tutorials from Unity3D.college
/// This script allows you to create your own object pool scripts from this template.
/// Inherit from this class to create any type of Object pool you want.
/// Make sure to clean up your objects before sending them back to the pool. This script
/// does not return object default settings: that is left to the user.
/// </summary>
public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T _prefab = null;
    [SerializeField] private int _startingPoolSize = 5;
    [SerializeField] private Transform _poolGroup = null;
    [SerializeField] private string _poolObjectName = "";

    private Queue<T> _objectPool = new Queue<T>();

    #region Initialization
    private void Awake()
    {
        if(_prefab == null)
        {
            Debug.LogError(this + ": no pool prefab defined");
            this.enabled = false;
            return;
        }

        if (_poolGroup == null)
        {
            _poolGroup = this.transform;
        }
        if (_poolObjectName == "")
        {
            _poolObjectName = _prefab.name;
        }
    }
    private void Start()
    {
        CreateInitialPool(_startingPoolSize);
    }
    private void CreateInitialPool(int startingPoolSize)
    {
        for (int i = 0; i < startingPoolSize; i++)
        {
            CreateNewPoolObject();
        }
    }
    #endregion

    #region Public
    /// <summary>
    /// Retrieve an deactivated object from the pool. Make sure you activate the
    /// GameObject when you want to use it.
    /// </summary>
    /// <returns></returns>
    public T Get()
    {
        if(_objectPool.Count == 0)
        {
            CreateNewPoolObject();
        }

        return _objectPool.Dequeue();
    }
    /// <summary>
    /// Disables an object and returns it to the pool. Make sure you return object
    /// to default settings if you've made any changes to the object's attributes.
    /// </summary>
    /// <param name="objectToReturn"></param>
    public void ReturnToPool(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        _objectPool.Enqueue(objectToReturn);
    }
    #endregion

    private void CreateNewPoolObject()
    {
        var newObject = GameObject.Instantiate(_prefab);

        newObject.transform.SetParent(_poolGroup);
        newObject.gameObject.name = _poolObjectName;
        newObject.gameObject.SetActive(false);
        _objectPool.Enqueue(newObject);
    }
}

