using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ACDev.Samples
{
    public class EnemyTurnState : IState
    {
        GameController _gameController;

        public event Action EnemyPlayedCard = delegate { };

        float _enemyTurnDelay = 1.5f;

        public EnemyTurnState(GameController gameController)
        {
            _gameController = gameController;
        }

        public IEnumerator Enter()
        {
            Debug.Log("...Entering EnemyTurn state");

            Debug.Log("Enemy making decision");
            yield return new WaitForSeconds(_enemyTurnDelay);
            Debug.Log("Enemy acted!");
            EnemyPlayedCard.Invoke();
        }

        public void Tick()
        {
            // we can even use Tick for a single frame, to transition after Enter() is done.
            // calling it this way just triggers state change as soon as it's able (and locks it
            // to prevent multiple calls)
            _gameController.ChangeState(_gameController.PlayerTurnState);
        }

        public IEnumerator Exit()
        {
            Debug.Log("Exiting EnemyTurn state...");
            yield break;
        }
    }
}

