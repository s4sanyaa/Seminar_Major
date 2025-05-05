using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject // not instantiated in game world - not attached to game object
{
    [SerializeField]
    float staminaCost = 10;
    AbilityComponent abilityComponent;

    bool abilityOnCooldown = false;

    internal void InitAbility(AbilityComponent abilityComponent)
    {
        this.abilityComponent = abilityComponent;
    }

    public abstract void ActivateAbility();

// check all the conditiion needed to activate the ability
    protected bool CommitAbility()
    {
        if (abilityOnCooldown) return false;

        if (abilityComponent == null || abilityComponent.TryConsumeStamina(staminaCost))
            return false;

        // start cooldown
        // ...

        return true;
    }
}
