using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public enum NodeResult
    {
        Success,
        Failure,
        Inprogress
    }

    public abstract class BTNode
    {
        
        public NodeResult UpdateNode()
        {
            // One-off thing
            if (!started)
            {
                started = true;
                NodeResult execResult = Execute();
                if (execResult != NodeResult.Inprogress)
                {
                    EndNode();
                    return execResult;
                }
            }

            // Time-based
            NodeResult updateResult = Update();
            if (updateResult != NodeResult.Inprogress)
            {
                EndNode();
            }

            return updateResult;

        }

        protected virtual NodeResult Update()
        {// Time-based
            return NodeResult.Success;
        }

        private void EndNode()
        {
            started = false;
            End();
        }

        protected virtual void End()
        {
            // clean up
            
        }

        public void Abort()
        {
            EndNode();
        }

        // override in child class 
        protected virtual NodeResult Execute()
        {
            return NodeResult.Success;
        }
         bool started = false;
         private int priority;
         public int GetPriority()
         {
             return priority;
         }

         public virtual void SortPriority(ref int priorityCounter)
         {
             priority = priorityCounter++;
             Debug.Log($"{this} has priority {priority}");
         }

         public virtual void Initialize()
         {
             
         }
         public virtual BTNode Get()
         {
             return this;
         }

    }


