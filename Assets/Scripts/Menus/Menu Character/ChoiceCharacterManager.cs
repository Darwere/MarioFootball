using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceCharacterManager : MonoBehaviour
{
    public static ChoiceCharacterManager instance;

    public bool player0Chose=false;
    public bool player1Chose=false;

    public GameObject MenuCharacterSelection;
    public GameObject PreviousCanvasMenuSelection;
    public AudioSource PreviousCanvasAudio;

    private void Awake()
    {
        instance = this;
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if(player0Chose && player1Chose)
        {
            SceneManager.LoadScene("Game",LoadSceneMode.Single);
        }
    }
    
}
