using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PerceptionComponent : MonoBehaviour
{
    [SerializeField] SenseComp[] senses;
    LinkedList<PerceptionStimuli> currentlyPerceivedStimulis = new LinkedList<PerceptionStimuli>();
    private PerceptionStimuli targetStimuli;

    public delegate void OnPerceptionTargetChanged(GameObject target, bool sensed);

    public event OnPerceptionTargetChanged onPerceptionTargetChanged;
    void Awake()
    {
        foreach (SenseComp sense in senses)
        {
            sense.onPerceptionUpdated += SenseUpdated;
        }
    }

    private void SenseUpdated(PerceptionStimuli stimuli, bool successfullySensed)
    { 
        var nodeFound = currentlyPerceivedStimulis.Find(stimuli);
        if (successfullySensed)
        {
           
            if (nodeFound != null)
            {
                currentlyPerceivedStimulis.AddAfter(nodeFound, stimuli);
            }
            else
            {
                currentlyPerceivedStimulis.AddLast(stimuli);
            }
        }
        else
        {
            currentlyPerceivedStimulis.Remove(nodeFound);
        }

        if (currentlyPerceivedStimulis.Count != 0)
        {
            PerceptionStimuli highestStimuli = currentlyPerceivedStimulis.First.Value;
    
            if (targetStimuli == null || targetStimuli != highestStimuli)
            {
                targetStimuli = highestStimuli;
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject,true);
            }
        }
        else
        {
            if (targetStimuli != null)
            {
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject,false);
                targetStimuli = null;
                
            }
        }

    }

    internal void AssignPerceivedStimuli(PerceptionStimuli targetStimuli)
    {
        if (senses.Length != 0)
        {
            senses[0].AssignPerceivedStimuli(targetStimuli);
        }
    }
}
