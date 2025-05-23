using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_RotateTowardsTarget : BTNode
{
    BehaviourTree tree;
    string targetKey;
    float acceptableDegrees;
    GameObject target;
    IBehaviourTreeInterface behaviorTreeInterface;

    public BTTask_RotateTowardsTarget(BehaviourTree tree, string targetKey, float acceptableDegrees = 10f)
    {
        this.tree = tree;
        this.targetKey = targetKey;
        this.acceptableDegrees = acceptableDegrees;

        this.behaviorTreeInterface = tree.GetBehaviourTreeInterface();
    }
    protected override NodeResult Execute()
    {
        if (tree == null || tree.Blackboard == null)
            return NodeResult.Failure;

        if (behaviorTreeInterface == null)
            return NodeResult.Failure;

        if (!tree.Blackboard.GetBlackboardData(targetKey, out target))
            return NodeResult.Failure;

        if (IsInAcceptableDegrees())
            return NodeResult.Success;

        tree.Blackboard.onBlackboardValueChange += BlackboardValueChanged;
        return NodeResult.Inprogress;
    }

    private void BlackboardValueChanged(string key, object val)
    {
        if (key == targetKey)
        {
            target = (GameObject)val;
        }
    }

    bool IsInAcceptableDegrees()
    {
        if (target == null) return false;

        Vector3 targetDir = (target.transform.position - tree.transform.position).normalized;
        Vector3 dir = tree.transform.forward;

        float degrees = Vector3.Angle(targetDir, dir);
        return degrees <= acceptableDegrees;
    }
    protected override NodeResult Update()
    {
        if (target == null)
            return NodeResult.Failure;

        if (IsInAcceptableDegrees())
            return NodeResult.Success;

        behaviorTreeInterface.RotateTowards(target);

        return NodeResult.Inprogress;
    }

    protected override void End()
    {
        
        tree.Blackboard.onBlackboardValueChange -= BlackboardValueChanged;
        base.End();
    }
}
