using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : Enemy
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform launchPoint;
    private Vector3 Destination;
    public override void AttackTarget(GameObject target)
    {
        Animator.SetTrigger("Attack");
        Destination = target.transform.position;
    }

    public void Shoot()
    {
        Projectile newProjectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);
        newProjectile.Launch(gameObject, Destination);
    }
}
