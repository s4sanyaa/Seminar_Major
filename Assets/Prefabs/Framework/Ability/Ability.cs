using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject // not instantiated in game world - not attached to game object
{
    [SerializeField] private Sprite AbilityIcon;
    [SerializeField] float staminaCost = 10;
    [SerializeField] private float cooldownDuration = 2f;
    
    public AbilityComponent AbilityComp
    {
        get{ return abilityComponent; }
        private set{ abilityComponent = value; }
    }
    public AbilityComponent abilityComp;

   AbilityComponent abilityComponent;

    bool abilityOnCooldown = false;

    public delegate void OnCooldownStarted();

    public OnCooldownStarted onCooldownStarted;

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
        StartAbilityCooldown();
        // ...

        return true;
    }
    void StartAbilityCooldown()
    {
       
       
        abilityComponent.StartCoroutine( CooldownCoroutine ( ) ) ;
    }

    internal Sprite GetAbilityIcon()
    {
        return AbilityIcon;
    }
    IEnumerator CooldownCoroutine()
    {
        abilityOnCooldown = true ;
        onCooldownStarted?.Invoke();
        yield return new WaitForSeconds( cooldownDuration ) ;
        abilityOnCooldown = false ;
      
    }
}
