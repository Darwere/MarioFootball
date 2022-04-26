using UnityEngine;

public class CanvasCharacter : MonoBehaviour
{
    public GameObject Canvas;
    public AudioSource SelectionAudio;
    public AudioSource ValidateChoiceAudio;
    
    public static CanvasCharacter instance;

    private void Awake()
    {
        instance = this;
    }
}
