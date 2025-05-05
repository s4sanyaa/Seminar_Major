using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    Ability ability;
    [SerializeField] Image AbilityIcon;
    [SerializeField] Image CooldownWheel;

    [SerializeField] float highlightSize = 1.5f;
    [SerializeField] float hightOffset = 200f;
    [SerializeField] float ScaleSpeed = 20f;
    [SerializeField] private RectTransform offsetPivot;

    Vector3 GoalScale = Vector3.one;
    Vector3 GoalOffset = Vector3.zero;

    private bool bIsOnCoolDown = false;
    private float CooldownCounter = 0f;
    internal void Init(Ability newAbility)
    {
        ability = newAbility;
        AbilityIcon.sprite = newAbility.GetAbilityIcon();
        CooldownWheel.enabled = false;
        ability.onCooldownStarted += StartCooldown;
    }
    public void SetScaleAmt(float amt)
    {
        GoalScale = Vector3.one * (1 + (highlightSize - 1) * amt);
        GoalOffset = Vector3.left * hightOffset * amt;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, GoalScale, Time.deltaTime * ScaleSpeed);
        offsetPivot.localPosition = Vector3.Lerp(offsetPivot.localPosition, GoalOffset, Time.deltaTime * ScaleSpeed);
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
