using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] private float TurnSpeed = 8f;

    public float RotateTowards(Vector3 aimDir)
    {
        float currentTurnSpeed = 0;
        if (aimDir.magnitude != 0)
        {
            Quaternion prevRotation = transform.rotation;
            float turnLerpAlpha = TurnSpeed * Time.deltaTime;
            transform.rotation =Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDir, Vector3.up),turnLerpAlpha);
            
            Quaternion currentRotation = transform.rotation;
            float Dir = Vector3.Dot(aimDir, transform.right) > 0 ? 1 : -1;
            float rotationDelta = Quaternion.Angle(prevRotation, currentRotation) * Dir;
            currentTurnSpeed = rotationDelta / Time.deltaTime;
            
           
        }

        return currentTurnSpeed;

    }
}
