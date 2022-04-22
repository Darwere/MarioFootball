using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceCharacterManager : MonoBehaviour
{
    public static ChoiceCharacterManager instance;

    public bool player0Chose=false;
    public bool player1Chose=false;

    private void Awake()
    {
        instance = this;
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if(player0Chose && player1Chose)
        {

      

            Debug.Log("bonjour");
            SceneManager.LoadScene("Ball",LoadSceneMode.Single);

        }
    }
    
}
