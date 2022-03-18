using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGrid : MonoBehaviour
{
    public GameObject PrefabCharacter;
    public List<CharacterData> Characters;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach(var character in Characters)
        {
            GameObject characterGo = GameObject.Instantiate(PrefabCharacter, transform);
            CharacterUI characterUI = characterGo.GetComponent<CharacterUI>();
            characterUI.Picture.sprite = character.Picture;
            //characterUI.Picture.GetComponent<RectTransform>().pivot = character.Offset;
            //characterUI.Picture.GetComponent<RectTransform>().sizeDelta = character.Size;
            characterUI.TextName.text = character.Name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
