using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_wait : BTNode
{
    private float WaitTime = 2f;
    private float TimeElapsed = 0f;

    public BTTask_wait(float waitTime)
    {
        this.WaitTime = waitTime;
    }
    protected override NodeResult Execute()
    {
        if (WaitTime <= 0)
        {
            return NodeResult.Success;
        }
        Debug.Log($"wait started with duration : {WaitTime}");
        TimeElapsed = 0f;
        return NodeResult.Inprogress;
    }

    protected override NodeResult Update()
    {
        TimeElapsed += Time.deltaTime;
        if (TimeElapsed >= WaitTime)
        { 
            Debug.Log($"wait finished");
            return NodeResult.Success;
           
        }
     //   Debug.Log($"waiting for {TimeElapsed}");
        return NodeResult.Inprogress;

    }
}
