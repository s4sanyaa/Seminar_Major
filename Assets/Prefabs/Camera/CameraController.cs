using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   [SerializeField] private Transform followTrans;
   [SerializeField] private float TurnSpeed = 2f;

   void LateUpdate()
   {
      transform.position = followTrans.position;
   }
   public void AddYawInput(float amt)
   {
      transform.Rotate(Vector3.up, amt * Time.deltaTime * TurnSpeed);
   }

}
