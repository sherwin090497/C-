using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Based on Brackey's Sound class in the Introduction to Audio in Unity video

[System.Serializable]
public class Sound
{
    // Clip, volume, pitch, name, loop, and source contain needed aspects of each audio clip.
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public string name;

    [HideInInspector]
    public AudioSource source;

    public bool loop;
}
