using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerSelectionWithIA : MonoBehaviour
{
    public Color colorSelection;
    public AudioSource SelectionAudio;
    public AudioSource ValidateAudio;

    private Menu menuAction;
    private GameObject characterSelected;
    private int counterCharacter = 0;

    private void Awake()
    {
        menuAction = new Menu();
    }
    private void Start()
    {
        SelectionCharacter();
    }

    public void ListenController(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        Vector2 positionJoystick = context.ReadValue<Vector2>();
        //Debug.Log(positionJoystick);
        if (Math.Abs(positionJoystick.y) > 0)
        {
            if (positionJoystick.x > 0.5)
            {
                SelectionCharacterRight(context);
            }
            else if (positionJoystick.x < -0.5)
            {
                SelectionCharacterLeft(context);
            }
        }

    }

    public void SelectionCharacter()
    {
        Debug.Log(counterCharacter);
        characterSelected = CharacterGrid.instance.listCharacters[counterCharacter];
        SelectionCharacterUI(characterSelected);
    }

    void SelectionCharacterUI(GameObject characterSelected)
    {
        characterSelected.GetComponent<Image>().color = Color.red;
        PlayAudio(SelectionAudio);
    }


    void DeselectionCharacterUI(GameObject characterSelected)
    {
        characterSelected.GetComponent<Image>().color = Color.black;
    }

    public void SelectionCharacterRight(InputAction.CallbackContext context)
    {

        DeselectionCharacterUI(CharacterGrid.instance.listCharacters[counterCharacter]);
        counterCharacter++;
        if (counterCharacter < CharacterGrid.instance.listCharacters.Count)
        {

            SelectionCharacter();
        }
        else
        {
            counterCharacter = 0;
            SelectionCharacter();
        }

    }

    public void SelectionCharacterLeft(InputAction.CallbackContext context)
    {
        DeselectionCharacterUI(CharacterGrid.instance.listCharacters[counterCharacter]);


        counterCharacter--;
        if (counterCharacter >= 0)
        {
            SelectionCharacter();
        }
        else
        {
            counterCharacter = CharacterGrid.instance.listCharacters.Count - 1;
            SelectionCharacter();
        }

    }

    public void ValidateChoice(InputAction.CallbackContext context)
    {
        PlayAudio(ValidateAudio);
        Match.instance.captain1 = characterSelected.GetComponent<PlayerSpecChoice>().PlayerSpecs;

        int randomCharacter = UnityEngine.Random.Range(0, 3);
        if (randomCharacter == counterCharacter)
        {
            int randomrCharacter = UnityEngine.Random.Range(0, 3);

        }
        counterCharacter = randomCharacter;
        Match.instance.captain2 = characterSelected.GetComponent<PlayerSpecChoice>().PlayerSpecs;
    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }

    public void OnEnable()
    {
        menuAction.ControlMenu.SelectionItemRigth.Enable();
        menuAction.ControlMenu.SelectionItemRigth.performed += SelectionCharacterRight;



        menuAction.ControlMenu.SelectionItemLeft.Enable();
        menuAction.ControlMenu.SelectionItemLeft.performed += SelectionCharacterLeft;



        menuAction.ControlMenu.SelectionItem.Enable();
        menuAction.ControlMenu.SelectionItem.performed += ListenController;

        menuAction.ControlMenu.Validate.Enable();
        menuAction.ControlMenu.Validate.performed += ValidateChoice;

    }
    private void OnDisable()
    {
        menuAction.ControlMenu.SelectionItemRigth.Disable();
        menuAction.ControlMenu.SelectionItemLeft.Disable();
        menuAction.ControlMenu.SelectionItem.Disable();
        menuAction.ControlMenu.Validate.Disable();
    }
}
