using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterGrid : MonoBehaviour
{
    public GameObject PrefabCharacter;
    GameObject characterSelected;
    public List<CharacterData> Characters;
    List<GameObject> listCharacters=new List<GameObject>();
    int compteurCharacter=0;
    // Start is called before the first frame update
    void Start()
    {
        foreach(var character in Characters)
        {
            
            GameObject characterGo = GameObject.Instantiate(PrefabCharacter, transform);
            listCharacters.Add(characterGo);
            CharacterUI characterUI = characterGo.GetComponent<CharacterUI>();
            characterUI.Picture.sprite = character.Picture;
            //characterUI.Picture.GetComponent<RectTransform>().pivot = character.Offset;
            //characterUI.Picture.GetComponent<RectTransform>().sizeDelta = character.Size;
            characterUI.TextName.text = character.Name;
        }
        SelectionCharacter();

    }

    // Update is called once per frame
    void Update()
    {
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
}
