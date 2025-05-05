using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    Ability ability;
    [SerializeField] Image AbilityIcon;
    [SerializeField] Image CooldownWheel;


    internal void Init(Ability newAbility)
    {
        ability = newAbility;
        AbilityIcon.sprite = newAbility.GetAbilityIcon();
        CooldownWheel.enabled = false;
        ability.onCooldownStarted += StartCooldown;
    }

    private void StartCooldown()
    {

    }
}
