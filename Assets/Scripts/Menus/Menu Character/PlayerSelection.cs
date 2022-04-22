using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    public Color SelectionColorPlayer1;
    public Color SelectionColorPlayer2;
    public GameObject HiderPrefab;
    public GameObject PositionVisualizationP1;
    public GameObject PositionVisualizationP2;
    public PlayerSpecs Mate1PlayerSpec;
    public PlayerSpecs Mate2PlayerSpec;
    public PlayerSpecs Goal1PlayerSpec;
    public PlayerSpecs Goal2PlayerSpec;

    private bool hasValidated = false;
    private bool canChose = true;
    private bool player0;
    private Vector3 positionVisualization;
    private Quaternion rotationVisualization;
    private AudioSource validateAudio;
    private AudioSource selectionAudio;
    private GameObject prefabCharacterSelected;
    private GameObject characterSelected;
    private Color colorOtherPlayer;
    private Color colorSelection;
    private int indexPlayer;
    private int counterCharacter = 0;

    //public void playerJoined(PlayerInput playerInput)
    //{
    //    Debug.Log(playerInput.playerIndex);
    //    Debug.Log("playerJoin");
    //    if (playerInput.playerIndex == 0)
    //    {
    //        player0 = true;
    //        //colorSelection = selectionColorPlayer1;
    //        colorSelection = Color.red;

    //    }
    //    else
    //    {

    //        colorSelection = Color.green;
    //        //colorSelection = selectionColorPlayer2;
    //    }
    //    SelectionCharacter();
    //}
    
    public void Start()
    {
        indexPlayer = GetComponent<PlayerInput>().playerIndex;
        selectionAudio = CanvasCharacter.instance.SelectionAudio;
        validateAudio = CanvasCharacter.instance.ValidateChoiceAudio;
        rotationVisualization = Quaternion.identity;
        if (indexPlayer == 0)
        {
            player0 = true;
            colorSelection = SelectionColorPlayer1;
            colorOtherPlayer = SelectionColorPlayer2;
            positionVisualization = PositionVisualizationP1.transform.position;
            //colorSelection = Color.red;

        }
        else
        {
            //colorSelection = Color.green;
            colorSelection = SelectionColorPlayer2;
            colorOtherPlayer = SelectionColorPlayer1;
            positionVisualization = PositionVisualizationP2.transform.position;
        }
        SelectionCharacter();
    }

    public void ListenController(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        if (canChose)
        {
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
    }

    public void SelectionCharacter()
    {
        if (CharacterGrid.instance.listCharacters[counterCharacter].GetComponent<Image>().color == colorOtherPlayer)
        {
            counterCharacter++;
        }
        characterSelected = CharacterGrid.instance.listCharacters[counterCharacter];
        SelectionCharacterUI(characterSelected);
    }

    void SelectionCharacterUI(GameObject characterSelected)
    {
        selectionAudio.Play();
        characterSelected.GetComponent<Image>().color = colorSelection;
        prefabCharacterSelected=Instantiate(characterSelected.GetComponent<PlayerSpecChoice>().PrefabVisualization, positionVisualization, rotationVisualization);
    }


    void DeselectionCharacterUI(GameObject characterSelected)
    {
        characterSelected.GetComponent<Image>().color = Color.black;
        rotationVisualization = characterSelected.transform.rotation;
        Debug.Log(rotationVisualization);
        Destroy(prefabCharacterSelected);
    }

    public void SelectionCharacterRight(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        if (canChose)
        {
            DeselectionCharacterUI(CharacterGrid.instance.listCharacters[counterCharacter]);

            counterCharacter++;

            if (counterCharacter >= CharacterGrid.instance.listCharacters.Count)
            {
                counterCharacter = 0;
                if (CharacterGrid.instance.listCharacters[counterCharacter].GetComponent<Image>().color == colorOtherPlayer)
                {
                    counterCharacter++;
                }
                SelectionCharacter();
            }
            else
            {
                if (CharacterGrid.instance.listCharacters[counterCharacter].GetComponent<Image>().color == colorOtherPlayer)
                {
                    counterCharacter++;
                }
                SelectionCharacter();
            }
        }

    }



    public void SelectionCharacterLeft(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (canChose)
        {
            DeselectionCharacterUI(CharacterGrid.instance.listCharacters[counterCharacter]);

            counterCharacter--;

            if (counterCharacter >= 0)
            {
                if (CharacterGrid.instance.listCharacters[counterCharacter].GetComponent<Image>().color == colorOtherPlayer)
                {
                    counterCharacter--;
                }
                SelectionCharacter();
            }
            else
            {
                counterCharacter = CharacterGrid.instance.listCharacters.Count - 1;
                if (CharacterGrid.instance.listCharacters[counterCharacter].GetComponent<Image>().color == colorOtherPlayer)
                {
                    counterCharacter--;
                }
                SelectionCharacter();
            }
        }

    }

    public void ValidateChoice()
    {
        if (!hasValidated)
        {
            if (player0)
            {
                validateAudio.Play();
                canChose = false;
                GameObject hider = Instantiate(HiderPrefab, characterSelected.transform.position, Quaternion.identity);
                hider.transform.SetParent(CanvasCharacter.instance.Canvas.transform, true);
                Match.instance.captain1 = characterSelected.GetComponent<PlayerSpecChoice>().PlayerSpecs;
                Match.instance.mate1 = Mate1PlayerSpec;
                Match.instance.goalKeeper1 = Goal1PlayerSpec;
                ChoiceCharacterManager.instance.player0Chose = true;
            }
            else
            {
                validateAudio.Play();
                canChose = false;
                GameObject hider = Instantiate(HiderPrefab, characterSelected.transform.position, Quaternion.identity);
                hider.transform.SetParent(CanvasCharacter.instance.Canvas.transform, true);
                Match.instance.captain2 = characterSelected.GetComponent<PlayerSpecChoice>().PlayerSpecs;
                Match.instance.mate2 = Mate2PlayerSpec;
                Match.instance.goalKeeper2 = Goal2PlayerSpec;
                ChoiceCharacterManager.instance.player1Chose = true;
            }
            hasValidated = true;
        }
        
    }
}
