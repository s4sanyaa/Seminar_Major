using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehaviour : BehaviourTree
{
    protected override void ConstructTree(out BTNode rootNode)
    {
        
       
        Selector RootSelector = new Selector();
        
        RootSelector.AddChild(new BTTask_GroupAttackTarget(this,2,10f));
        RootSelector.AddChild(new BTTaskGroup_MoveToLastseenLoc(this,3));
        RootSelector.AddChild(new BTTaskGroup_Patrolling(this,3));


   
        rootNode =RootSelector;

    }
}

