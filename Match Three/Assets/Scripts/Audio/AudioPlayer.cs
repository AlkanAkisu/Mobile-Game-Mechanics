using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioManager audioManager;

    [SerializeField] private GameObject audioManagerPrefab;

    [SerializeField] private Component _component;

    [SerializeField] private bool isDebug;
    [SerializeField, ShowIf(nameof(isDebug))] private string name;

    private AudioManager AudioManager
    {
        get
        {
            if (audioManager != null)
                return audioManager;
            audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null)
                return audioManager;
            audioManager = Instantiate(audioManagerPrefab).GetComponent<AudioManager>();
            audioManager.UpdateSources();
            return audioManager;
        }
    }
    
    private static AudioPlayer _instance;
    public static AudioPlayer i => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }
    
    public Sound GetSound(string name) => AudioManager.GetSound(name);

    public void Play(string name) => AudioManager.Play(name);

    public void Stop(string name) => AudioManager.Stop(name);

    //   ---------------        DEBUG       ---------------   //
    [NaughtyAttributes.Button, ShowIf(nameof(isDebug))]
    public void StopAll() => AudioManager.StopAll();
    [NaughtyAttributes.Button, ShowIf(nameof(isDebug))]
    private void Play() => Play(name);
    [NaughtyAttributes.Button, ShowIf(nameof(isDebug))]
    private void Stop() => Stop(name);
}
