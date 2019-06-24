using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACDev.Utility;

namespace ACDev.Samples
{
    public class UtilityTests : MonoBehaviour
    {
        [SerializeField] CubePool _cubePool = null;

        SampleCube _currentCube = null;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _currentCube = _cubePool.Get();
                _currentCube.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                _cubePool.ReturnToPool(_currentCube);
                _currentCube = null;
            }
        }
    }
}

