using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PreviousCanvas : MonoBehaviour
{
    public GameObject ActualCanvas;
    public GameObject PreviousCanvasObject;
    public AudioSource PreviousCanvasAudio;

    private Menu menuAction;

    private void Awake()
    {
        menuAction = new Menu();
    }

    public void ChangePreviousCanvas(InputAction.CallbackContext context)
    {

        PreviousCanvasObject.SetActive(true);
        ActualCanvas.SetActive(false);
    }
    public void ValidatePlayAudio(InputAction.CallbackContext context)
    {

        PreviousCanvasAudio.Play();
    }

    private void OnEnable()
    {
        menuAction.ControlMenu.PreviousSceneChangement.Enable();
        menuAction.ControlMenu.PreviousSceneChangement.performed += ChangePreviousCanvas;
        menuAction.ControlMenu.PreviousSceneChangement.performed += ValidatePlayAudio;
    }
    private void OnDisable()
    {
        menuAction.ControlMenu.PreviousSceneChangement.Disable();
    }
}
