using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ACDev.Samples
{
    [System.Serializable]
    public class EnemyTurnState : IState
    {
        [SerializeField] float _enemyTurnDelay = 1.5f;

        GameController _owner;

        public EnemyTurnState(GameController owner)
        {
            _owner = owner;
        }

        public void Enter()
        {
            Debug.Log("...Entering EnemyTurn state");

            Debug.Log("Enemy making decision");
            Debug.Log("Wait: " + _enemyTurnDelay + " seconds...");
            Debug.Log("Enemy acted!");
        }

        public void Tick()
        {
            // we can even use Tick for a single frame, to transition after Enter() is done.
            // calling it this way just triggers state change as soon as it's able (and locks it
            // to prevent multiple calls)
            _owner.ChangeState(_owner.PlayerTurnState);
        }

        public void Exit()
        {
            Debug.Log("Exiting EnemyTurn state...");
        }
    }
}

