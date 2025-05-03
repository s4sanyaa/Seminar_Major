using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_AttackTarget : BTNode
{
    BehaviourTree tree;
    string targetKey;
    GameObject target;

    public BTTask_AttackTarget(BehaviourTree tree, string targetKey)
    {
        this.tree = tree;
        this.targetKey = targetKey;
    }

    protected override NodeResult Execute()
    {
        if (!tree || tree.Blackboard == null || 
            !tree.Blackboard.GetBlackboardData(targetKey, out target))
            return NodeResult.Failure;

        IBehaviourTreeInterface BehaviourInterface = tree.GetBehaviourTreeInterface();
        if (BehaviourInterface == null)
            return NodeResult.Failure;

        BehaviourInterface.AttackTarget(target);
        return NodeResult.Success;
    }

}
