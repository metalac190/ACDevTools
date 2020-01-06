using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This adds auto-rotate behaviour to a Rigidbody object
/// Created by: Adam Chandler
/// Apply this script to an object with a Rigidbody to make it rotate on play;
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class RotatorRB : MonoBehaviour
{
    [SerializeField] Vector3 _rotateDirection = new Vector3(0, 1, 0);
    [SerializeField] float _rotateSpeed = 25;

    Rigidbody _rigidbody = null;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
    }

    private void FixedUpdate()
    {
        Vector3 _amountToRotate = _rotateDirection * _rotateSpeed * Time.fixedDeltaTime;
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_amountToRotate));
    }
}

