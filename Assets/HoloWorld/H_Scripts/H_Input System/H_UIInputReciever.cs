using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class H_UIInputReciever : H_InputReciever
{
    [SerializeField] UnityEvent onClick;

    public override void OnInputRecieved()
    {
        foreach (var handler in inputHandlers)
        {
            handler.ProcessInput(Input.mousePosition, gameObject, () => onClick.Invoke());
        }
    }
}
