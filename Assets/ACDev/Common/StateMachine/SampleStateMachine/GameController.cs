using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACDev.Samples
{
    // TODO find a better implementation through a subscription/dictionary model, instead of random Properties
    public class GameController : StateMachine
    {
        // we can store info and pass this down into the state, if we want
        public string PlayerName { get; private set; } = "Adam the Mighty";

        // ensure that the state always exists, if something tries to access it
        PlayerTurnState _playerTurnState;
        public PlayerTurnState PlayerTurnState 
        {
            get
            {
                if (_playerTurnState == null)
                {
                    _playerTurnState = new PlayerTurnState(this);
                }
                return _playerTurnState;
            }
        }
        // ensure that the state always exists, if something tries to access it
        EnemyTurnState _enemyTurnState;
        public EnemyTurnState EnemyTurnState
        {
            get
            {
                if(_enemyTurnState == null)
                {
                    _enemyTurnState = new EnemyTurnState(this);
                }
                return _enemyTurnState;
            }
        }

        private void Start()
        {
            ChangeState(PlayerTurnState);
        }
    }
}

