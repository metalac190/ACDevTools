using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACDev.Samples
{
    // TODO find a better implementation through a subscription/dictionary model, instead of random Properties
    public class GameController : StateMachine
    {
        public event Action BeginEnemyPhase;

        AudioSource _audioSource;

        [SerializeField] PlayerTurnState _playerTurnState;
        public PlayerTurnState PlayerTurnState { get => _playerTurnState; }

        [SerializeField] EnemyTurnState _enemyTurnState;
        public EnemyTurnState EnemyTurnState { get => _enemyTurnState; }

        private void Awake()
        {
            // get components here, if desired
            _audioSource = GetComponent<AudioSource>();
            // set up your states
            _playerTurnState = new PlayerTurnState(this, _audioSource);
            _enemyTurnState = new EnemyTurnState(this);
        }

        private void Start()
        {
            ChangeState(PlayerTurnState);
        }
    }
}

