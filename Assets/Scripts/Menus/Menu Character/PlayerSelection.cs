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

    private bool player0;
    private GameObject characterSelected;
    private int counterCharacter = 0;

    public void playerJoined(PlayerInput playerInput)
    {
        Debug.Log(playerInput.playerIndex);
        Debug.Log("playerJoin");
        if (playerInput.playerIndex == 0)
        {
            player0 = true;
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

        characterSelected = CharacterGrid.instance.listCharacters[counterCharacter];

        SelectionCharacterUI(characterSelected);
    }

    void SelectionCharacterUI(GameObject characterSelected)
    {
            characterSelected.GetComponent<Image>().color = colorSelection;
    }


    void DeselectionCharacterUI(GameObject characterSelected)
    {
        characterSelected.GetComponent<Image>().color = Color.black;
    }

    public void SelectionCharacterRight()
    {

        DeselectionCharacterUI(CharacterGrid.instance.listCharacters[counterCharacter]);
        counterCharacter++;

        if (CharacterGrid.instance.listCharacters[counterCharacter].GetComponent<Image>().color != Color.black)
        {
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

        else
        {
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

        
    }


    public void SelectionCharacterLeft()
    {
        DeselectionCharacterUI(CharacterGrid.instance.listCharacters[counterCharacter]);
        counterCharacter--;

        if (CharacterGrid.instance.listCharacters[counterCharacter].GetComponent<Image>().color != Color.black)
        {
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

        else
        {
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
    }

    public void ValidateChoice()
    {
        if (player0)
        {
            Match.instance.captain1 = characterSelected.GetComponent<PlayerSpecChoice>().PlayerSpecs;
            ChoiceCharacterManager.instance.player0Chose = true;
        }
        else
        {
            Match.instance.captain2 = characterSelected.GetComponent<PlayerSpecChoice>().PlayerSpecs;
            ChoiceCharacterManager.instance.player1Chose = true;
        }
        //SceneManager.LoadScene("Ball 1");
    }
}
