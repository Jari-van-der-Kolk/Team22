using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public SoundEffect[] sounds;

    private static SoundManager singleton;
    public static SoundManager Singleton
    {
        get
        {
            return singleton;
        }

        private set
        {
            if (singleton)
            {
                Destroy(value);
                Debug.LogError("We have more than one DustManager!!!");
                return;
            }

        }
    }



    private void Awake()
    {
        singleton = this;

        //makes AudioSource components on this gameobject and initializes their settings from the SciptableObject
        foreach (SoundEffect s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //put any type of background music here
    }


    public void Play(AudioClip clip)
    {
        SoundEffect s = Array.Find(sounds, sound => sound.clip ==  clip);
        if (s == null)
        {
            Debug.LogWarning(s.name + " This soundeffect name is currently not in use!");
            return;
        }
        s.source.PlayOneShot(s.source.clip);
    }
}
