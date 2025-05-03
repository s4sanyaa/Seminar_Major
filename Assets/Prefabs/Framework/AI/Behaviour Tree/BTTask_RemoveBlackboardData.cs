using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_RemoveBlackboardData : BTNode
{
    BehaviourTree tree;
    string keyToRemove;

    public BTTask_RemoveBlackboardData(BehaviourTree tree, string keyToRemove)
    {
        this.tree = tree;
        this.keyToRemove = keyToRemove;
    }

    protected override NodeResult Execute()
    {
        if (tree != null && tree.Blackboard != null)
        {
            tree.Blackboard.RemoveBlackboardData(keyToRemove);
            return NodeResult.Success;
        }

        return NodeResult.Failure;
    }

}
