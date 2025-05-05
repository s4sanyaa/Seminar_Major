using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityDock : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] AbilityComponent abilityComponent;
    [SerializeField] RectTransform Root;
    [SerializeField] VerticalLayoutGroup LayoutGrp;
    [SerializeField] AbilityUI AbilityUIPrefab;

    [SerializeField] private float ScaleRange = 200f;
    List<AbilityUI> abilityUIs = new List<AbilityUI>();
    private PointerEventData touchData;
    private AbilityUI hightlightedAbility;
    
    
    [SerializeField] float highlightSize = 1.5f;
    private Vector3 goalScale = Vector3.one;
    [SerializeField] float ScaleSpeed = 20f;

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

    void Update()
    {
        if(touchData!=null)
        {
            GetUIUnderPointer(touchData, out hightlightedAbility);
            ArrangeScale(touchData);
        }

        transform.localScale = Vector3.Lerp(transform.localScale, goalScale, Time.deltaTime * ScaleSpeed);
    }

    private void ArrangeScale ( PointerEventData touchData )
    {

        if ( ScaleRange == 0 ) return ;

        float touchVerticalPos = touchData.position.y ;

        foreach ( AbilityUI abilityUI in abilityUIs )

        {

            float abilityUIVerticalPos = abilityUI.transform.position.y ;

            float distance = Mathf . Abs ( touchVerticalPos - abilityUIVerticalPos ) ;

            if ( distance > ScaleRange )

            {

                abilityUI.SetScaleAmt ( 0 ) ;

                continue ;

            }

            float scaleAmt = ( ScaleRange - distance ) / ScaleRange ;
            abilityUI.SetScaleAmt ( scaleAmt ) ;

        }

    }   

    public void OnPointerDown(PointerEventData eventData)
    {
        touchData = eventData;
        goalScale = Vector3.one * highlightSize;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(hightlightedAbility)
        {
            hightlightedAbility.ActivateAbility();
        }
        touchData = null;
        ResetScale();
        goalScale = Vector3.one;
    }

    private void ResetScale()
    {
        foreach(AbilityUI abilityUI in abilityUIs)
        {
            abilityUI.SetScaleAmt(0);
        }
    }
    bool GetUIUnderPointer(PointerEventData eventData, out AbilityUI abilityUI)
    {
        List<RaycastResult> findAbility = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, findAbility);

        abilityUI = null;
        foreach(RaycastResult result in findAbility)
        {
            abilityUI = result.gameObject.GetComponentInParent<AbilityUI>();
            if (abilityUI != null)
                return true;
        }

        return false;
    }
}
