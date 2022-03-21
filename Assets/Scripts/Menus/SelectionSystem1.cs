using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSystem1 : MonoBehaviour
{
    public List<GameObject> ListItems = new List<GameObject>();
    public List<GameObject> ListCanvasItems = new List<GameObject>();
    public GameObject ActualCanvas;

    int compteurItem = 0;
    GameObject itemSelected;

    void Start()
    {
        
        SelectionItem();

    }

    public void SelectionItem()
    {
        itemSelected = ListItems[compteurItem];
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
        DeselectionItemUI(ListItems[compteurItem]);
        compteurItem++;
        if (compteurItem < ListItems.Count)
        {

            SelectionItem();
        }
        else
        {
            compteurItem = 0;
            SelectionItem();
        }
    }

    public void SelectionItemDown()
    {
        DeselectionItemUI(ListItems[compteurItem]);
        compteurItem--;

        if (compteurItem >= 0)
        {
            SelectionItem();
        }
        else
        {
            compteurItem = ListItems.Count - 1;
            SelectionItem();

        }
    }

    public void Validate()
    {
        GameObject nextCanvas = ListCanvasItems[compteurItem];
        nextCanvas.SetActive(true);
        ActualCanvas.SetActive(false);
    }

}
