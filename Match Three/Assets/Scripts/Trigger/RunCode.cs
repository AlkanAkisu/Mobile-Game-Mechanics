using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class RunCode : MonoBehaviour
{
    [SerializeField] private bool onAwake;
    [SerializeField,ShowIf(nameof(onAwake))] private UnityEvent runAwake;
    [SerializeField] private bool onStart;
    [SerializeField,ShowIf(nameof(onStart))] private UnityEvent runStart;
    [SerializeField] private bool onEnable;
    [SerializeField,ShowIf(nameof(onEnable))] private UnityEvent runEnable;
    [SerializeField] private bool onDisable;
    [SerializeField,ShowIf(nameof(onDisable))] private UnityEvent runDisable;

    void Awake()
    {
        if(onAwake)
            runAwake?.Invoke();
    }
    void Start()
    {
        if(onStart)
            runStart?.Invoke();
    }

    private void OnEnable()
    {
        if(onEnable)
            runEnable?.Invoke();
    }

    private void OnDisable()
    {
        if(onDisable)
            runDisable?.Invoke();
    }



}
