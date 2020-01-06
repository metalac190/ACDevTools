using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This adds auto-rotate behaviour to a transform (NOT physics/Rigidbody)
/// Created by: Adam Chandler
/// Apply this script that does NOT have a Rigidbody. This is useful for visuals that
/// do not need dynamic colliders, but could benefit from visual movement.
/// </summary>


public class RotatorTransform2D : MonoBehaviour
{
    [SerializeField] float _rotateSpeed = 25;

    void Update()
    {
        Vector3 direction = new Vector3(0, 0, 1);
        transform.Rotate(direction * _rotateSpeed * Time.deltaTime);
    }
}
