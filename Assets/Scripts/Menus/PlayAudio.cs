using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource audioSource;
    
    void Start()
    {
    }

    public void Audio()
    {
        audioSource.Play();
    }
}
