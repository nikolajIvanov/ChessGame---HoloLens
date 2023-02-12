using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(H_Board))]
public class H_BoardInputHandler : MonoBehaviour, H_IInputHandler
{
    private H_Board board;

    private void Awake()
    {
        board = GetComponent<H_Board>();
    }

    public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action onClick)
    {
        board.OnSquareSelected(inputPosition);
    }
}
