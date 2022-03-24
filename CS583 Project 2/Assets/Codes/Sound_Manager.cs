using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{

    public static AudioClip coinSound;
    public static AudioClip healSound;
    public static AudioClip boostSound;
    public static AudioClip scrapeSound;

    static AudioSource source;

    void Start()
    {
        coinSound = Resources.Load<AudioClip>("Coin-Sound");
        healSound = Resources.Load<AudioClip>("Heal-Sound");
        boostSound = Resources.Load<AudioClip>("Boost-Sound");
        scrapeSound = Resources.Load<AudioClip>("Scraping-Sound");

        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "Coin-Sound":
                source.PlayOneShot(coinSound);
                break;
            case "Heal-Sound":
                source.PlayOneShot(healSound);
                break;
            case "Boost-Sound":
                source.PlayOneShot(boostSound);
                break;
            case "Scraping-Sound":
                source.PlayOneShot(scrapeSound);
                break;
        }
    }
}
