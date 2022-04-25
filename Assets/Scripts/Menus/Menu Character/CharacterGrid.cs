using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterGrid : MonoBehaviour
{
    public static CharacterGrid instance;

    public List<GameObject> listCharacters = new List<GameObject>();
    public GameObject PrefabCharacter;
    public List<CharacterData> Characters;

   
    

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        foreach(var character in Characters)
        {          
            GameObject characterGo = GameObject.Instantiate(PrefabCharacter, transform);
            listCharacters.Add(characterGo);

            characterGo.AddComponent<PlayerSpecChoice>();
            characterGo.GetComponent<PlayerSpecChoice>().PlayerSpecs=character.PlayerSpecs;

            characterGo.GetComponent<PlayerSpecChoice>().PrefabVisualization = character.PrefabVisualization;

            CharacterUI characterUI = characterGo.GetComponent<CharacterUI>();
            characterUI.Picture.sprite = character.Picture;
            //characterUI.Picture.GetComponent<RectTransform>().pivot = character.Offset;
            //characterUI.Picture.GetComponent<RectTransform>().sizeDelta = character.Size;
            characterUI.TextName.text = character.Name;
        }
        //SelectionCharacter();
    }

    
}
