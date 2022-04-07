using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectionSystem : MonoBehaviour
{
    public List<GameObject> ListButtons = new List<GameObject>();

    
    private int compteurButtons = 0;
    private GameObject buttonSelected;
    PlayerInput playerInput;


    public void SelectionCharacter()
    {
        buttonSelected = ListButtons[compteurButtons];
        SelectionCharacterUI(buttonSelected);
    }

    void SelectionCharacterUI(GameObject buttonSelected)
    {
        buttonSelected.GetComponent<Image>().color = Color.red;
    }
    void DeselectionCharacterUI(GameObject buttonSelected)
    {
        buttonSelected.GetComponent<Image>().color = Color.black;
    }

    public void SelectionCharacterRight()
    {
        Debug.Log(playerInput.playerIndex);
        DeselectionCharacterUI(ListButtons[compteurButtons]);
        compteurButtons++;
        if (compteurButtons < ListButtons.Count)
        {

            SelectionCharacter();
        }
        else
        {
            compteurButtons = 0;
            SelectionCharacter();
        }
    }

    public void SelectionCharacterLeft()
    {
        DeselectionCharacterUI(ListButtons[compteurButtons]);
        compteurButtons--;

        if (compteurButtons >= 0)
        {
            SelectionCharacter();
        }
        else
        {
            compteurButtons = ListButtons.Count - 1;
            SelectionCharacter();

        }
    }
}
