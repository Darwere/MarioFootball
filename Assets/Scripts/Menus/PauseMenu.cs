using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    Menu menuActions;

    private void Awake()
    {
        menuActions = new Menu();
    }

    private void backToGame(InputAction.CallbackContext context)
    {
        UIManager.BackToGame();
    }

    private void replay(InputAction.CallbackContext context)
    {
        BackToMenu.LoadSceneMenu(context);
    }

    private void OnEnable()
    {
        menuActions.ControlMenu.PreviousSceneChangement.Enable();
        menuActions.ControlMenu.PreviousSceneChangement.performed += backToGame;

        menuActions.ControlMenu.Validate.Enable();
        menuActions.ControlMenu.Validate.performed += replay;
    }

    private void OnDisable()
    {
        menuActions.ControlMenu.PreviousSceneChangement.Disable();
        menuActions.ControlMenu.Validate.Disable();
    }
}
