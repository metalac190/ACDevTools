using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACDev.Utility
{
    public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private T _prefab = null;

        public static GenericObjectPool<T> Instance { get; private set; }
        private Queue<T> objects = new Queue<T>();

        private void Awake()
        {
            Instance = this;
        }

        #region Public
        public T Get()
        {
            if(objects.Count == 0)
            {
                AddObject();
            }

            return objects.Dequeue();
        }

        public void ReturnToPool(T objectToReturn)
        {
            objectToReturn.gameObject.SetActive(false);
            objects.Enqueue(objectToReturn);
        }
        #endregion

        private void AddObject()
        {
            var newObject = GameObject.Instantiate(_prefab);
            newObject.gameObject.SetActive(false);
            objects.Enqueue(newObject);
        }
    }
}

