using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ACDev.Samples
{
    public class PlayerTurnState : IState
    {
        public event Action PlayerTurnStart;
        public event Action PlayerTurnEnd;
        // hold on to references here
        GameController _owner;
        // pass whatever information you need into the constructor
        public PlayerTurnState(GameController owner)
        {
            _owner = owner;
        }

        public IEnumerator Enter()
        {
            Debug.Log("PlayerTurn has started!");
            PlayerTurnStart?.Invoke();
            Debug.Log("Player Name: " + _owner.PlayerName);
            yield break;
        }

        public void Tick()
        {
            
        }

        public IEnumerator Exit()
        {
            Debug.Log("PlayerTurn has ended!");
            PlayerTurnEnd?.Invoke();
            yield break;
        }
    }
}

