using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACDev.Samples
{
    public class PoolingController : MonoBehaviour
    {
        [SerializeField] ExamplePool _cubePool = null;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PooledObject newPooledObject = _cubePool.GetObject();
                newPooledObject.DoThing();
                StartCoroutine(RemoveObject(newPooledObject, 1.5f));
            }
        }

        IEnumerator RemoveObject(PooledObject pooledObject, 
            float timeUntilRemove)
        {
            // wait, then return object to pool
            yield return new WaitForSeconds(timeUntilRemove);

            _cubePool.ReturnObject(pooledObject);
        }
    }
}

