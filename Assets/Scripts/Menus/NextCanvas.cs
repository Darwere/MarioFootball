using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NextCanvas : MonoBehaviour
{
    public GameObject ActualCanvas;
    public GameObject NextCanvasObject;
    public AudioSource NextCanvasAudio;

    private Menu menuAction;

    private void Awake()
    {
        menuAction = new Menu();
    }
    public void ChangeNextCanvas(InputAction.CallbackContext context)
    {

        NextCanvasObject.SetActive(true);
        ActualCanvas.SetActive(false);
    }


    public void ValidatePlayAudio(InputAction.CallbackContext context)
    {
        NextCanvasAudio.Play();
    }

    private void OnEnable()
    {

        menuAction.ControlMenu.NextSceneChangement.Enable();
        menuAction.ControlMenu.NextSceneChangement.performed += ChangeNextCanvas;
        menuAction.ControlMenu.NextSceneChangement.performed += ValidatePlayAudio;


    }
    private void OnDisable()
    {
        menuAction.ControlMenu.NextSceneChangement.Disable();

    }
}