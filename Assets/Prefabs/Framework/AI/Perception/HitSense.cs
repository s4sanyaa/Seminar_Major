using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSense : SenseComp
{
    [SerializeField] private HealthComponent HealthComponent;
    [SerializeField] private float HitMemory = 2f;
    private Dictionary<PerceptionStimuli, Coroutine> HitRecord = new Dictionary<PerceptionStimuli, Coroutine>();
    protected override bool IsStimuliSensable(PerceptionStimuli stimuli)
    {
        return HitRecord.ContainsKey(stimuli);
    }

    private void Start()
    {
        HealthComponent.onTakeDamage += TookDamage;
    }

   
    private void TookDamage(float health, float delta, float maxHealth, GameObject Instigator)
    {
        PerceptionStimuli stimuli = Instigator.GetComponent<PerceptionStimuli>();

        if (stimuli != null)
        {
            Coroutine newForgettingCoroutine = StartCoroutine(ForgetStimuli(stimuli));

            if (HitRecord.TryGetValue(stimuli, out Coroutine onGoingCoroutine))
            {
                StopCoroutine(onGoingCoroutine);
                HitRecord[stimuli] = newForgettingCoroutine;
            }
            else
            {
                HitRecord.Add(stimuli,newForgettingCoroutine);
            }
        }
    }

    IEnumerator ForgetStimuli(PerceptionStimuli Stimuli)
    {
        yield return new WaitForSeconds(HitMemory);
        HitRecord.Remove(Stimuli);
    }

}
