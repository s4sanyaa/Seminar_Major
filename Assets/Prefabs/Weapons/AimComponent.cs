using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private float aimRange = 1000f;
    [SerializeField] private LayerMask aimMask;
    public GameObject GetAimTarget(out Vector3 aimDir)
    {
        Vector3 aimStart = muzzle.position;
      //  Vector3 aimDir = muzzle.forward;
      aimDir = GetAimDir();
        if (Physics.Raycast(aimStart, aimDir, out RaycastHit hitInfo, aimRange, aimMask))
        {
            return hitInfo.collider.gameObject;
        }

        return null;

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(muzzle.position, muzzle.position + GetAimDir() * aimRange);
    }
    Vector3 GetAimDir()
    {
        Vector3 muzzleDir = muzzle.forward;
        return new Vector3(muzzleDir.x, 0f, muzzleDir.z).normalized;
    }

}
