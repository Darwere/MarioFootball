using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectionSystem1 : MonoBehaviour
{
    public List<GameObject> ListItems = new List<GameObject>();
    public List<GameObject> ListCanvasItems = new List<GameObject>();
    public GameObject ActualCanvas;

    
    int counterItem = 0; //Variable qui parcourt la liste
    GameObject itemSelected;

    void Start()
    {

        SelectItem(); //Selection du premier item

    }

    public void ListenController(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        Vector2 positionJoystick = context.ReadValue<Vector2>(); 
        Debug.Log(positionJoystick);
        if (Math.Abs(positionJoystick.x) > 0)
        {
            if (positionJoystick.y > 0.5)
            {
                
                SelectionItemUp();

            }
            else if (positionJoystick.y < -0.5)
            {
                
                SelectionItemDown();

            }
        }

    }

    public void SelectItem() //Selection de l'item
    {
        itemSelected = ListItems[counterItem];
        SelectionItemUI(itemSelected);
    }

    void SelectionItemUI(GameObject itemSelected)
    {
        itemSelected.GetComponent<Image>().color = Color.red;
    }
    void DeselectionItemUI(GameObject itemSelected)
    {
        itemSelected.GetComponent<Image>().color = Color.black;
    }

    public void SelectionItemUp()
    {

        DeselectionItemUI(ListItems[counterItem]);
        counterItem++;
        if (counterItem < ListItems.Count)
        {

            SelectItem();
        }
        else
        {
            counterItem = 0;
            SelectItem();
        }
    }

    public void SelectionItemDown()
    {

        DeselectionItemUI(ListItems[counterItem]);
        counterItem--;

        if (counterItem >= 0)
        {
            SelectItem();
        }
        else
        {
            counterItem = ListItems.Count - 1;
            SelectItem();

        }




    }

    public void Validate()
    {
        GameObject nextCanvas = ListCanvasItems[counterItem];
        nextCanvas.SetActive(true);
        ActualCanvas.SetActive(false);
    }


}
