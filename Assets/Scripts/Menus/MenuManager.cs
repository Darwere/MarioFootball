using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public GameObject ActualCanvas;
    public GameObject NextCanvas;
    public GameObject PreviousCanvas;
    public AudioSource ValidateAudio;

    private Menu menuAction;

    private void Awake()
    {
        menuAction = new Menu();
    }
    public void ChangeNextCanvas(InputAction.CallbackContext context)
    {
        
        NextCanvas.SetActive(true);
        ActualCanvas.SetActive(false);
    }

    public void ChangePreviousCanvas()
    {
        
        PreviousCanvas.SetActive(true);
        ActualCanvas.SetActive(false); 
    }
    public void ValidatePlayAudio(InputAction.CallbackContext context)
    {
        
        ValidateAudio.Play();
    }

    private void OnEnable()
    {
        
        menuAction.ControlMenu.Validate.Enable();
        menuAction.ControlMenu.Validate.performed += ChangeNextCanvas;
        menuAction.ControlMenu.Validate.performed += ValidatePlayAudio;





    }
    private void OnDisable()
    {
        menuAction.ControlMenu.NextSceneChangement.Disable();
    }

}
