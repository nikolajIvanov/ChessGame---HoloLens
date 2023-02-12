using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_UIInputHandler : MonoBehaviour, H_IInputHandler
{
    public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action onClick)
    {
        onClick?.Invoke();
    }
}
