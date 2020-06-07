using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ACDev.Samples
{
    public class PlayerTurnState : IState
    {
        AudioSource _audioSource;

        // hold on to references here
        GameController _owner;
        // pass whatever information you need into the constructor
        public PlayerTurnState(GameController owner, AudioSource audioSource)
        {
            _owner = owner;
            _audioSource = audioSource;
        }

        public void Enter()
        {
            Debug.Log("PlayerTurn has started!");

            _audioSource.Play();
        }

        public void Tick()
        {
            
        }

        public void Exit()
        {
            Debug.Log("PlayerTurn has ended!");
        }
    }
}

