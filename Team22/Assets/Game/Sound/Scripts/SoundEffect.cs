using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundEffect")]
public class SoundEffect : ScriptableObject
{
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(1f, 3f)] public float pitch = 1f;
    public bool loop;

    [HideInInspector]
    public AudioSource source;


    internal void PlaySound()
    {
        if (source == null)
        {
            Debug.LogWarning("SoundManager does not exist");
            return;
        }
        
        SoundManager.Singleton.Play(clip);
    }
}
