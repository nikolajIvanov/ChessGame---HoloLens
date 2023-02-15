using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public abstract class HoloLensInputReciever : MonoBehaviour, IMixedRealityPointerHandler
{
    protected IInputHandler[] inputHandlers;

    private void Awake()
    {
        inputHandlers = GetComponents<IInputHandler>();
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        foreach (var handler in inputHandlers)
        {
            handler.ProcessInput(eventData.Pointer.Result.Details.Point, gameObject, () => {});
        }
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData) { }
    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData) { }
}
