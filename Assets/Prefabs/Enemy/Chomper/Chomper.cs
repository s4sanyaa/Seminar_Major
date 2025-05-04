using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Search;

public class Chomper : Enemy
{
    [SerializeField] private TriggerDamageComponent damageComponent;
  public override void AttackTarget(GameObject target)
  {
    Animator.SetTrigger("Attack");
  }

  public void AttackPoint()
  {
    if (damageComponent)
    {
      damageComponent.SetDamageEnabled(true);
    }
  }

  public void AttackEnd()
  {
    if (damageComponent)
    {
      damageComponent.SetDamageEnabled(false);
    }
  }

 protected override void Start() 
  {
    base.Start();
    damageComponent.SetTeamInterfaceSrc(this);
  }
}
