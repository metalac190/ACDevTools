using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This adds auto-rotate behaviour to a 2D transform (NOT physics/Rigidbody)
/// Created by: Adam Chandler
/// Apply this script to a 2D object that does NOT have a Rigidbody. This is useful for visuals that
/// do not need dynamic colliders, but could benefit from visual movement.
/// </summary>

public class RotatorTransform : MonoBehaviour
{
    [SerializeField] Vector3 _rotateDirection = new Vector3(0, 1, 0);
    [SerializeField] float _rotateSpeed = 25;

    void Awake()
    {
        CheckForRigidbody();
    }

    void CheckForRigidbody()
    {
        if(GetComponent<Rigidbody>() != null)
        {
            // there's a rigidbody attached. Don't manipulate transform
            this.enabled = false;
        }
    }

    void Update()
    {
        transform.Rotate(_rotateDirection * _rotateSpeed * Time.deltaTime);
    }
}

