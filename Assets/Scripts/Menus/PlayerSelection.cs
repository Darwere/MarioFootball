using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    public Color selectionColorPlayer1;
    public Color selectionColorPlayer2;


    public Color colorSelection;
    private GameObject characterSelected;
    private int compteurCharacter = 0;

    public void playerJoined(PlayerInput playerInput)
    {
        Debug.Log(playerInput.playerIndex);
        Debug.Log("playerJoin");
        if (playerInput.playerIndex == 0)
        {

            //colorSelection = selectionColorPlayer1;
            colorSelection = Color.red;
            
        }
        else
        {

            colorSelection = Color.green;
            //colorSelection = selectionColorPlayer2;
        }
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
                SelectionCharacterRight();
            }
            else if (positionJoystick.x < -0.5)
            {
                SelectionCharacterLeft();
            }
        }

    }
    
    public void SelectionCharacter()
    {

        characterSelected = CharacterGrid.instance.listCharacters[compteurCharacter];

        SelectionCharacterUI(characterSelected);
    }

    void SelectionCharacterUI(GameObject characterSelected)
    {
        Debug.Log(colorSelection);
        characterSelected.GetComponent<Image>().color = colorSelection;
    }
    void DeselectionCharacterUI(GameObject characterSelected)
    {
        characterSelected.GetComponent<Image>().color = Color.black;
    }

    public void SelectionCharacterRight()
    {

        DeselectionCharacterUI(CharacterGrid.instance.listCharacters[compteurCharacter]);
        compteurCharacter++;
        if (compteurCharacter < CharacterGrid.instance.listCharacters.Count)
        {

            SelectionCharacter();
        }
        else
        {
            compteurCharacter = 0;
            SelectionCharacter();
        }
    }


    public void SelectionCharacterLeft()
    {
        DeselectionCharacterUI(CharacterGrid.instance.listCharacters[compteurCharacter]);
        compteurCharacter--;

        if (compteurCharacter >= 0)
        {
            SelectionCharacter();
        }
        else
        {
            compteurCharacter = CharacterGrid.instance.listCharacters.Count - 1;
            SelectionCharacter();

        }
    }

    public void ValidateChoice()
    {
        Match.instance.captain1 = characterSelected.GetComponent<PlayerSpecChoice>().PlayerSpecs;
        SceneManager.LoadScene("Ball 1");
    }
}
