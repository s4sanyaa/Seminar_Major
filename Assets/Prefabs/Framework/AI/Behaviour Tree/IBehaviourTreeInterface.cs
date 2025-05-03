using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviourTreeInterface
{
    public void RotateTowards(GameObject target, bool VerticalAim = false);
    public void AttackTarget(GameObject target);

}
