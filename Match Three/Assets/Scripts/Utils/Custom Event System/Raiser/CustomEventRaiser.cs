using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine.Events;

public class CustomEventRaiser : MonoBehaviour
{
    [SerializeField] CustomEvent Event;
    [SerializeField] private bool isDebug;


    [Button,ShowIf(nameof(isDebug))]
    public void RaiseCustomEvent()
    {
        Event?.Raise();
    }
}