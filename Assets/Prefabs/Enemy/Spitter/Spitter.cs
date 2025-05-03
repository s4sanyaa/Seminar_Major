using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : Enemy
{
    public override void AttackTarget(GameObject target)
    {
        Animator.SetTrigger("Attack");
    }

    public void Shoot()
    {
        Debug.Log("spitter shooting");
    }
}
