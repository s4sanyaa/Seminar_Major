using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDock : MonoBehaviour
{
    [SerializeField] AbilityComponent abilityComponent;
    [SerializeField] RectTransform Root;
    [SerializeField] VerticalLayoutGroup LayoutGrp;
    [SerializeField] AbilityUI AbilityUIPrefab;

    List<AbilityUI> abilityUIs = new List<AbilityUI>();

    private void Awake()
    {
        abilityComponent.onNewAbilityAdded += AddAbility;
    }

    private void AddAbility(Ability newAbility)
    {
        AbilityUI newAbilityUI = Instantiate(AbilityUIPrefab, Root);
        newAbilityUI.Init(newAbility);
        abilityUIs.Add(newAbilityUI);
    }
}
