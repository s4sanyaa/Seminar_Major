using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decorator : BTNode
{
      BTNode child;

     protected BTNode GetChild()
     {
          return child;
     }
     public Decorator(BTNode child)
     {
          this.child = child;
     }

     public override BTNode Get()
     {
          return child.Get();
     }

     public override void Initialize()
     {
          base.Initialize();
          child.Initialize();
     }

     public override void SortPriority(ref int priorityCounter)
     {
          base.SortPriority(ref priorityCounter);
          child.SortPriority(ref priorityCounter);
     }
}
