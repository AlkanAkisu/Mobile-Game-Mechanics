using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public AudioClip clip;
    public string name;

    [Range(0f, 1f)] public float volume = 0.3f;

    public float pitch = 1;

    public bool loop;

    public AudioSource source;

    public Sound WithPitch(float pitch)
    {
        source.pitch = pitch;
        return this;
    }

    public Sound WithPitchRate(float pitchRate)
    {
        source.pitch = source.pitch * pitchRate;
        return this;
    }

    public Sound WithVolume(float volume)
    {
        source.volume = volume;
        return this;
    }

    public Sound WithVolumeRate(float volumeRate)
    {
        source.volume = source.volume * volumeRate;
        return this;
    }

    public void Play()
    {
        source.Play();
    }

    private void BackDefaults()
    {
        source.volume = volume;
        source.pitch = pitch;
    }


    public void Stop()
    {
        source.Stop();
    }
}