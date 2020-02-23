using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACDev.Samples
{
    public class ExampleSMController : MonoBehaviour
    {
        [SerializeField] GameController _controller = null;

        private void OnEnable()
        {
            _controller.EnemyTurnState.EnemyPlayedCard += OnEnemyMove;
        }

        private void OnDisable()
        {
            _controller.EnemyTurnState.EnemyPlayedCard -= OnEnemyMove;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // pressed input! Player has "passed" turn
                _controller.ChangeState(_controller.EnemyTurnState);
            }
        }

        void OnEnemyMove()
        {
            Debug.Log("UI: Enemy movied! Simulate UI Panel Animations");
        }
    }
}

