using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip sounds;
    public AudioSource source;

    void Start()
    {
        source = this.GetComponent<AudioSource>();
        sounds = this.GetComponent<AudioSource>().clip;
    }
    
    
}
