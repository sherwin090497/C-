using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

// Based on Brackey's Audio Manager class in his video Introduction to Audio in Unity
public class AudioManager : MonoBehaviour
{
    public static AudioClip coinSound;
    public static AudioClip hitSound;
    public static AudioClip fireballSound;
    public static AudioClip powerupSound;
    public static AudioClip npcdeathSound;
    public static AudioClip enemyDeath;
    public static AudioClip gemSound;
    static AudioSource source;
    public Sound[] sounds;

    // Singleton Pattern
    public static AudioManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        // Iterate through all of the sounds to initialize all of the sources elements
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Update is called once per frame

    // Similar to Brackey's Play function in the video Introduction to Audio in Unity
    // Play a sound based on the name. If the name doesn't exist,
    // display a warning and return.
    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + soundName + " was not found.");
            return;
        }
        s.source.Play();
    }

    // Stop an audio source if the name exits.
    public void Stop(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + soundName + " was not found.");
            return;
        }
        s.source.Stop();
    }

    // Stop all audio sources
    public void StopAll()
    {
        foreach(Sound s in sounds)
        {
            s.source.Stop();
        }
    }

    // At the beginning, play the main theme.
    void Start()
    {
        
        
    }

 
}
