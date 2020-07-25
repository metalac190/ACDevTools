using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACDev.Samples
{
    public class ExamplePool : MBObjectPool<PooledObject>
    {
        protected override void ResetObjectDefaults(PooledObject pooledObject)
        {
            // optionally do stuff here, if needed to reset state
        }
    }
}

