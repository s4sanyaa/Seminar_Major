using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform ThumbStickTrans;
    [SerializeField] private RectTransform BackgroundTrans;
    [SerializeField] private RectTransform CenterTrans;

    private bool bWasDragging;
    public delegate void OnStickInputValueUpdated(Vector2 inputValue);
    public delegate void OnStickTap();

    public event OnStickInputValueUpdated OnStickValueUpdated;
    public event OnStickTap onStickTap;
    public void OnDrag(PointerEventData eventData)
    {
       Vector2 touchPos = eventData.position;
        Vector2 centerPos = BackgroundTrans.position;
        Vector2 localOffset = Vector2.ClampMagnitude(touchPos - centerPos, BackgroundTrans.sizeDelta.x/4);
        Vector2 InputValue = localOffset / (BackgroundTrans.sizeDelta.x / 2);
        ThumbStickTrans.position = centerPos + localOffset;
        OnStickValueUpdated?.Invoke(InputValue);
        // Debug.Log($"On Drag Fired {eventData.position}");
        bWasDragging = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       // Debug.Log("PointerDown");
       BackgroundTrans.position = eventData.position;
       ThumbStickTrans.position = eventData.position;
       bWasDragging = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      //  Debug.Log("PointerUp");
      BackgroundTrans.position = CenterTrans.position;
      ThumbStickTrans.position = BackgroundTrans.position;
      OnStickValueUpdated?.Invoke(Vector2.zero);
      if (!bWasDragging)
      {
          onStickTap?.Invoke();
      }
    }
}
