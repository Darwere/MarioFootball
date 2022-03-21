using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionSystem : MonoBehaviour
{
    public List<GameObject> ListButtons = new List<GameObject>();
    int compteurButtons = 0;
    GameObject buttonSelected;


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
