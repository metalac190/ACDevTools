using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACDev.Samples
{
    public class ExampleSMController : MonoBehaviour
    {
        [SerializeField] GameController _controller = null;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // pressed input! Player has "passed" turn
                _controller.ChangeState(_controller.EnemyTurnState);
            }
        }
    }
}

