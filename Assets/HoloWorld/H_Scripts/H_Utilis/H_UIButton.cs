using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(H_UIInputReciever))]
public class H_UIButton : Button
{
    private H_InputReciever reciever;
    protected override void Awake()
    {
        reciever = GetComponent<H_UIInputReciever>();
        onClick.AddListener(() => reciever.OnInputRecieved());
    }
}
