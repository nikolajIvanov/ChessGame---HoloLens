using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ChangeMaterialOnTouch : MonoBehaviour
{
    [SerializeField]
    private Material touchedMaterial; // Material, das bei Berührung angezeigt wird

    private Material originalMaterial; // Original-Material des Game Objects

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material; // Original-Material des Game Objects holen
    }

    public void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        GetComponent<Renderer>().material = touchedMaterial; // Material des Game Objects ändern
    }

    public void OnTouchCompleted(HandTrackingInputEventData eventData)
    {
        GetComponent<Renderer>().material = originalMaterial; // Material des Game Objects auf das Original zurücksetzen
    }
}
