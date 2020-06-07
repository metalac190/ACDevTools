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

        public PlayerTurnState PlayerTurnState { get; private set; }

        public EnemyTurnState EnemyTurnState { get; private set; }

        private void Awake()
        {
            // get components here, if desired
            _audioSource = GetComponent<AudioSource>();
            // set up your states
            PlayerTurnState = new PlayerTurnState(this, _audioSource);
            EnemyTurnState = new EnemyTurnState(this);
        }

        private void Start()
        {
            ChangeState(PlayerTurnState);
        }
    }
}

