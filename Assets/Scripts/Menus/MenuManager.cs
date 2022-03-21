using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject ActualCanvas;
    public GameObject NextCanvas;
    public GameObject PreviousCanvas;
    
    public void ChangeNextCanvas()
    {
        
        NextCanvas.SetActive(true);
        ActualCanvas.SetActive(false);
    }

    public void ChangePreviousCanvas()
    {
        PreviousCanvas.SetActive(true);
        ActualCanvas.SetActive(false); 
    }
}
