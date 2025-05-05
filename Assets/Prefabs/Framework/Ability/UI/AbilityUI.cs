using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    Ability ability;
    [SerializeField] Image AbilityIcon;
    [SerializeField] Image CooldownWheel;


    private bool bIsOnCoolDown = false;
    private float CooldownCounter = 0f;
    internal void Init(Ability newAbility)
    {
        ability = newAbility;
        AbilityIcon.sprite = newAbility.GetAbilityIcon();
        CooldownWheel.enabled = false;
        ability.onCooldownStarted += StartCooldown;
    }

    private void StartCooldown()
    {

        if (bIsOnCoolDown) return;
        StartCoroutine(CooldownCoroutine());
    }

    IEnumerator CooldownCoroutine()
    {
        bIsOnCoolDown = true;
        CooldownCounter = ability.GetCoolDownDuration();
        float cooldownDuration = CooldownCounter;
        CooldownWheel.enabled = true;
        while (CooldownCounter > 0)
        {
            CooldownCounter -= Time.deltaTime;
            CooldownWheel.fillAmount = CooldownCounter / cooldownDuration;
            yield return new WaitForEndOfFrame();
        }

        bIsOnCoolDown = false;
        CooldownWheel.enabled = false;
    }

    internal void ActivateAbility()
    {
        ability.ActivateAbility();
        
    }
}
