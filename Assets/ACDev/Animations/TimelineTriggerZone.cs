using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

/// <summary>
/// This script triggers a specified Timeline director, when attached to a trigger
/// volume, and the conditions are met.
/// Repurposed from the Unity 3DGameKit.
/// </summary>
namespace ACDev.Animations
{
    [RequireComponent(typeof(Collider))]
    public class TimelineTriggerZone : MonoBehaviour
    {
        public enum TriggerType
        {
            Once, Everytime,
        }

        [Tooltip("This is the gameobject which will trigger the director to play.  For example, the player.")]
        [SerializeField] GameObject _triggeringGameObject = null;
        [SerializeField] PlayableDirector _director = null;
        [SerializeField] TriggerType _triggerType = TriggerType.Once;

        public UnityEvent OnDirectorPlay;
        public UnityEvent OnDirectorFinish;

        bool _alreadyTriggered = false;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != _triggeringGameObject)
                return;

            if (_triggerType == TriggerType.Once && _alreadyTriggered)
                return;

            OnDirectorPlay.Invoke();
            _director.Play();
            _alreadyTriggered = true;
            Invoke("FinishInvoke", (float)_director.duration);
        }

        void FinishInvoke()
        {
            OnDirectorFinish.Invoke();
        }
    }
}

