using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    private Menu inputActions;

    private void Awake()
    {
        inputActions = new Menu();
    }
    public static void LoadSceneMenu(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnEnable()
    {
        inputActions.ControlMenu.Validate.Enable();
        inputActions.ControlMenu.Validate.performed += LoadSceneMenu;
    }

    private void OnDisable()
    {
        inputActions.ControlMenu.Validate.Disable();
    }
}
