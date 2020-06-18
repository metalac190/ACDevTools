using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorHelper
{
    public static Vector3 AlignDirectionRelativeToTransform(Vector3 movement, Transform otherTransform)
    {
        Vector3 horizontalMovement = otherTransform.right * movement.x;
        Vector3 forwardMovement = otherTransform.forward * movement.z;
        // get a combined direction with normalized length
        Vector3 moveDirection = (horizontalMovement + forwardMovement).normalized;

        return moveDirection;
    }
}
