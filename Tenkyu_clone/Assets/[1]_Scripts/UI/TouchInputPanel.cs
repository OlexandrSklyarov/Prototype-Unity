using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInputPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    #region Event

    public event Action<Vector2> OnDragPointer;
    public event Action<bool> OnPressedPointer;

    #endregion


    #region Pointer  

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPressedPointer?.Invoke(true);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        OnPressedPointer?.Invoke(false);
    }

    #endregion


    #region

    public void OnDrag(PointerEventData eventData)
    {
        var dir = new Vector2(eventData.delta.x, eventData.delta.y);
        OnDragPointer?.Invoke(dir);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        var dir = new Vector2(eventData.delta.x, eventData.delta.y);
        OnDragPointer?.Invoke(dir);
    }

    #endregion
}
