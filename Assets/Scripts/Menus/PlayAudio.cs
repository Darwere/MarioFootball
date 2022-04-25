using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayAudio : MonoBehaviour
{
    public AudioSource audioSource;

    
   
    public void Audio(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        audioSource.Play();
    }

   
}
