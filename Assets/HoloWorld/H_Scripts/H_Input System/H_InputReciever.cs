using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class H_InputReciever : MonoBehaviour
{
    protected H_IInputHandler[] inputHandlers;

    public abstract void OnInputRecieved();

    private void Awake()
    {
        inputHandlers = GetComponents<H_IInputHandler>();
    }
}
