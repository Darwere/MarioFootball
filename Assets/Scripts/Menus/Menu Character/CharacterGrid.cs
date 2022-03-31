using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterGrid : MonoBehaviour
{
    public GameObject PrefabCharacter;
    public List<CharacterData> Characters;

    private GameObject characterSelected;
    private List<GameObject> listCharacters=new List<GameObject>();
    private int compteurCharacter=0;
    
    void Start()
    {
        foreach(var character in Characters)
        {
            
            GameObject characterGo = GameObject.Instantiate(PrefabCharacter, transform);
            listCharacters.Add(characterGo);

            characterGo.AddComponent<PlayerSpecChoice>();
            characterGo.GetComponent<PlayerSpecChoice>().PlayerSpecs=character.PlayerSpecs;

            CharacterUI characterUI = characterGo.GetComponent<CharacterUI>();
            characterUI.Picture.sprite = character.Picture;
            //characterUI.Picture.GetComponent<RectTransform>().pivot = character.Offset;
            //characterUI.Picture.GetComponent<RectTransform>().sizeDelta = character.Size;
            characterUI.TextName.text = character.Name;
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
        characterSelected = listCharacters[compteurCharacter];
        SelectionCharacterUI(characterSelected);
    }

    void SelectionCharacterUI(GameObject characterSelected)
    {
        characterSelected.GetComponent<Image>().color = Color.red;
    }
    void DeselectionCharacterUI(GameObject characterSelected)
    {
        characterSelected.GetComponent<Image>().color = Color.black;
    }

    public void SelectionCharacterRight()
    {
        DeselectionCharacterUI(listCharacters[compteurCharacter]);
        compteurCharacter++;
        if (compteurCharacter < listCharacters.Count)
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
        DeselectionCharacterUI(listCharacters[compteurCharacter]);
        compteurCharacter--;
        
        if (compteurCharacter >= 0)
        {
            SelectionCharacter();
        }
        else
        {
            compteurCharacter = listCharacters.Count-1;
            SelectionCharacter();

        }
    }

    public void ValidateChoice()
    {
        Match.instance.captain1=characterSelected.GetComponent<PlayerSpecChoice>().PlayerSpecs;
        SceneManager.LoadScene("Ball 1");
    }
}
