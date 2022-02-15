using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;


    [SerializeField] private Sound[] sounds;
    [SerializeField] private string debug;
    [SerializeField] private bool isDebug;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            UpdateSources();
            DontDestroyOnLoad(gameObject);
        }
    }


    [Button]
    [ShowIf(nameof(isDebug))]
    public void UpdateSources()
    {
        DeleteAllAudioSources();
        _instance = this;
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public Sound GetSound(string name)
    {
        return Array.Find(sounds, sound => sound.name == name);
    }


    public void Play(string name)
    {
        var s = GetSound(name);
        s.Play();
    }

    public void PlayOneShot(string name)
    {
        var s = Array.Find(sounds, sound => sound.name == name);
        s.source.PlayOneShot(s.clip);
    }

    public void Stop(string name)
    {
        var s = GetSound(name);
        s.Stop();
    }

    public void FadeIn(string name)
    {
        var s = GetSound(name);
        s.source.volume = 0;
        s.source.DOFade(s.volume, s.volume * 1f);
        s.Play();
    }

    public void FadeOut(string name)
    {
        var s = GetSound(name);
        s.source.DOFade(0, s.volume * 1f);
        s.Play();
    }

    [Button]
    [ShowIf(nameof(isDebug))]
    public void DebugPlay()
    {
        Play(debug);
    }

    [Button]
    [ShowIf(nameof(isDebug))]
    public void StopAll()
    {
        foreach (var sound in sounds) Stop(sound.name);
    }

    [Button]
    [ShowIf(nameof(isDebug))]
    public void DeleteAllAudioSources()
    {
        var audioSources = gameObject.GetComponents<AudioSource>();
        if (!Application.isPlaying)
            Array.ForEach(audioSources, aud => DestroyImmediate(aud));
        else
            Array.ForEach(audioSources, aud => Destroy(aud));
    }
}