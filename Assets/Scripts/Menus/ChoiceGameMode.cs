using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceGameMode : MonoBehaviour
{
    public static bool ModeIA = false;

    private void OnSceneGame(Scene scene, LoadSceneMode mode)
    {
        if (ModeIA)
        {
            Field.Team2.SetIABrain();
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneGame;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneGame;
    }
}
