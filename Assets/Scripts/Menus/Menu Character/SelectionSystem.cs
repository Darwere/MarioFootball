using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionSystem : MonoBehaviour
{
    public GraphicRaycaster Raycaster;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PointerEventData eventData = new PointerEventData(null);
        //eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        Raycaster.Raycast(eventData, results);

        foreach (var item in results)
        {
            Debug.Log(item);
        }
    }
}
