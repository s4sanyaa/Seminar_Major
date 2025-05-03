using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SenseComp : MonoBehaviour
{
    private static List<PerceptionStimuli> registeredStimuliList = new List<PerceptionStimuli>();
    private List<PerceptionStimuli> PerceivableStimuliList = new List<PerceptionStimuli>();
    [SerializeField] private float forgettingTime = 3f;
    private Dictionary<PerceptionStimuli, Coroutine> ForgettingRoutines = new Dictionary<PerceptionStimuli, Coroutine>();

    public delegate void OnPerceptionUpdated(PerceptionStimuli stimuli, bool successfullySensed);

    public event OnPerceptionUpdated onPerceptionUpdated;
    static public void RegisterStimuli(PerceptionStimuli stimuli)
    {
        if (registeredStimuliList.Contains(stimuli))
        {
            return;
        }
        registeredStimuliList.Add(stimuli);
    }
    
    static public void UnRegisterStimuli(PerceptionStimuli stimuli)
    {
        
        registeredStimuliList.Remove(stimuli);
    }

    protected abstract bool IsStimuliSensable(PerceptionStimuli stimuli);

    private void Update()
    {
        foreach (var stimuli in registeredStimuliList)
        {
            if (IsStimuliSensable(stimuli))
            {
                if (!PerceivableStimuliList.Contains(stimuli))
                {
                    PerceivableStimuliList.Add(stimuli);
                    if (ForgettingRoutines.TryGetValue(stimuli,out Coroutine routine))
                    {
                        StopCoroutine(routine);
                        ForgettingRoutines.Remove(stimuli);
                    }
                    else
                    {
                        onPerceptionUpdated?.Invoke(stimuli,true);
                   //     Debug.Log($"I just sensed {stimuli.gameObject}");

                    }
                }
            }
            else
            {
                if (PerceivableStimuliList.Contains(stimuli))
                {
                    PerceivableStimuliList.Remove(stimuli);

                    ForgettingRoutines.Add(stimuli,StartCoroutine(ForgetStimuli(stimuli)));
                }
            }
            
        }
        
    }

    IEnumerator ForgetStimuli(PerceptionStimuli stimuli)
    {
        yield return new WaitForSeconds(forgettingTime);
        ForgettingRoutines.Remove(stimuli);
        onPerceptionUpdated?.Invoke(stimuli,false);
     //    Debug.Log($"I just lost track of  {stimuli.gameObject}");
    }

    protected virtual void DrawDebug()
    {
        
    }

    private void OnDrawGizmos()
    {
        DrawDebug();
    }
}
