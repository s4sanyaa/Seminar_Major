using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class CameraArm : MonoBehaviour
{
   [SerializeField] private float armLength;
   [SerializeField] private Transform child;

   private void Update()
   {
      child.position = transform.position - child.forward * armLength;
   }
}
