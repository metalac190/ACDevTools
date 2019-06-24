using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This adds auto-rotate behaviour to a Rigidbody2D object
/// Created by: Adam Chandler
/// Apply this script to an object with a Rigidbody2D to make it rotate on play;
/// </summary>
namespace ACDev.Animations
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RotatorRB2D : MonoBehaviour
    {
        [SerializeField] float _rotateSpeed = 25;

        Rigidbody2D _rigidbody2D = null;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _rigidbody2D.gravityScale = 0;
            _rigidbody2D.isKinematic = true;
        }

        private void FixedUpdate()
        {
            _rigidbody2D.MoveRotation(_rigidbody2D.rotation + _rotateSpeed * Time.fixedDeltaTime);
        }
    }
}
