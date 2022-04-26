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
        Field.UnPause();
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
    }

    private void OnDisable()
    {
        menuActions.ControlMenu.PreviousSceneChangement.Disable();
    }
}
