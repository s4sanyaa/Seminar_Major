using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree : MonoBehaviour
{
    private BTNode Root;
    Blackboard blackboard = new Blackboard();

    private IBehaviourTreeInterface BehaviourTreeInterface;
 //   private BTNode prevNode;
    public Blackboard Blackboard
    {
        get { return blackboard; }
    }

    void Start()
    {
        BehaviourTreeInterface = GetComponent<IBehaviourTreeInterface>();
        ConstructTree(out Root);
        SortTree();
    }

    private void SortTree()
    {
        int priorityCounter = 0;
        Root.Initialize();
        Root.SortPriority(ref priorityCounter);
    }

    protected abstract void ConstructTree(out BTNode rootNode);

    public void AbortLowerThan(int priority)
    {
        BTNode currentNode = Root.Get();
        if (currentNode.GetPriority() > priority)
        {
            Root.Abort();
        }
    }

    void Update()
    {
        Root.UpdateNode();
        // BTNode currentNode = Root.Get();
        // if (prevNode != currentNode)
        // {
        //     prevNode = currentNode;
        //     Debug.Log($"current node changed to {currentNode}");
        // }
    }

    internal IBehaviourTreeInterface GetBehaviourTreeInterface()
    {
        return BehaviourTreeInterface;
    }
}
