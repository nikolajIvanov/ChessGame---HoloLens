using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_InstantTweener : MonoBehaviour, H_IObjectTweener
{
    public void MoveTo(Transform transform, Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }
}
