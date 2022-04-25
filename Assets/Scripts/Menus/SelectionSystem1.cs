using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectionSystem1 : MonoBehaviour
{
    public Color colorSelection;
    public List<GameObject> ListItems = new List<GameObject>();
    public List<GameObject> ListCanvasItems = new List<GameObject>();
    public GameObject ActualCanvas;
    public AudioSource SelectionAudio;
    public AudioSource ValidateAudio;

    public static SelectionSystem1 instance;

    private Menu menuAction;
    private int counterItem = 0; //Variable qui parcourt la liste
    private GameObject itemSelected;
    private bool firstSelection=true;

    private void Awake()
    {
        menuAction = new Menu();
        instance = this;
    }
    void Start()
    {

        SelectItem(); //Selection du premier item

    }

    public void ListenController(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        Vector2 positionJoystick = context.ReadValue<Vector2>(); 
        //Debug.Log(positionJoystick);
        if (Math.Abs(positionJoystick.x) > 0)
        {
            if (positionJoystick.y > 0.5)
            {
                SelectionItemUp(context);

            }
            else if (positionJoystick.y < -0.5)
            {
                SelectionItemDown(context);

            }
        }

    }

    public void SelectItem() //Selection de l'item
    {
        itemSelected = ListItems[counterItem];
        SelectionItemUI(itemSelected);
    }

    void SelectionItemUI(GameObject itemSelected)
    {
        itemSelected.GetComponent<Image>().color = colorSelection;
        if (!firstSelection)
        {
            PlayAudio(SelectionAudio);
        }
        else
        {
            firstSelection = false;
        }
    }
    void DeselectionItemUI(GameObject itemSelected)
    {
        itemSelected.GetComponent<Image>().color = Color.black;
    }

    public void SelectionItemUp(InputAction.CallbackContext context)
    {

        DeselectionItemUI(ListItems[counterItem]);
        counterItem++;
        if (counterItem < ListItems.Count)
        {
            SelectItem();
        }
        else
        {
            counterItem = 0;
            SelectItem();
        }
    }

    public void SelectionItemDown(InputAction.CallbackContext context)
    {
        DeselectionItemUI(ListItems[counterItem]);
        counterItem--;

        if (counterItem >= 0)
        {
            SelectItem();
        }
        else
        {
            counterItem = ListItems.Count - 1;
            SelectItem();

        }

    }

    public void Validate(InputAction.CallbackContext context)
    {
        PlayAudio(ValidateAudio);
        GameObject nextCanvas = ListCanvasItems[counterItem];
        nextCanvas.SetActive(true);
        ActualCanvas.SetActive(false);
    }


    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }
    public void OnEnable()
    {
        menuAction.ControlMenu.SelectionItemDown.Enable();
        menuAction.ControlMenu.SelectionItemDown.performed += SelectionItemDown;
        


        menuAction.ControlMenu.SelectionItemUp.Enable();
        menuAction.ControlMenu.SelectionItemUp.performed+=SelectionItemUp;
        


        menuAction.ControlMenu.SelectionItem.Enable();
        menuAction.ControlMenu.SelectionItem.performed+=ListenController;

        menuAction.ControlMenu.Validate.Enable();
        menuAction.ControlMenu.Validate.performed+=Validate;

    }
    private void OnDisable()
    {
        menuAction.ControlMenu.SelectionItemDown.Disable();
        menuAction.ControlMenu.SelectionItemUp.Disable();
        menuAction.ControlMenu.SelectionItem.Disable();
        menuAction.ControlMenu.Validate.Disable();

    }
}
