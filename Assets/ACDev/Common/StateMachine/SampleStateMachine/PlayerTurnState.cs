using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ACDev.Samples
{
    [System.Serializable]
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

        public IEnumerator Enter()
        {
            Debug.Log("PlayerTurn has started!");

            _audioSource.Play();

            yield break;
        }

        public void Tick()
        {
            
        }

        public IEnumerator Exit()
        {
            Debug.Log("PlayerTurn has ended!");
            yield break;
        }
    }
}

