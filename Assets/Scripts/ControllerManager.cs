using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerManager : MonoBehaviour
{
    int control1;
    
    //PlayerInputManager inputManager;

    //private void Start()
    //{
    //    inputManager = GetComponent<PlayerInputManager>();

    //}
    //public void Update()
    //{
    //    inputManager.onPlayerJoined += playerJoined;
    //}

    public void playerJoined(PlayerInput playerInput)
    {
        Debug.Log(playerInput.playerIndex);
    }

}
