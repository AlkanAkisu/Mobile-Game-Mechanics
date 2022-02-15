using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private bool isOneTime;
    [SerializeField] private bool onEnter;
    [SerializeField,ShowIf(nameof(onEnter))] private UnityEvent onTriggerEnter;
    [SerializeField] private bool onStay;
    [SerializeField,ShowIf(nameof(onStay))] private UnityEvent onTriggerStay;
    [SerializeField] private bool onExit;
    [SerializeField,ShowIf(nameof(onExit))] private UnityEvent onTriggerExit;

    [SerializeField] private bool filterByComponent;
    [SerializeField, ShowIf(nameof(filterByComponent))] private Component component;
    [SerializeField] private bool filterByTag;
    [SerializeField, Tag, ShowIf(nameof(filterByTag))] private string tagFilter;
    private bool enterTriggered;
    private bool exitTriggered;
    private bool stayTriggered;


    private void Awake()
    {
        enterTriggered = false;
        exitTriggered = false;
        stayTriggered = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!onEnter)
            return;
        if(isOneTime && enterTriggered)
            return;
        if(filterByComponent && col.GetComponent(component.GetType()) == null)
            return;
        if(filterByTag && !col.CompareTag(tagFilter))
            return;
        
        onTriggerEnter?.Invoke();
        enterTriggered = true;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(!onStay)
            return;
        if(isOneTime && stayTriggered)
            return;
        if(filterByComponent && col.GetComponent(component.GetType()) == null)
            return;
        if(filterByTag && !col.CompareTag(tagFilter))
            return;
        
        onTriggerStay?.Invoke();
        stayTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(!onExit)
            return;
        if(isOneTime && exitTriggered)
            return;
        if(filterByComponent && col.GetComponent(component.GetType()) == null)
            return;
        if(filterByTag && !col.CompareTag(tagFilter))
            return;
        
        onTriggerExit?.Invoke();
        exitTriggered = true;
    }
}
