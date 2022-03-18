using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject ActualCanvas;
    public GameObject NextCanvas;
    // Start is called before the first frame update
    
    public void ChangeCanvas()
    {
        Debug.Log("canvas change");

        NextCanvas.SetActive(true);
        ActualCanvas.SetActive(false);
    }
}
