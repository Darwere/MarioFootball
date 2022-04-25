using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingItem : MonoBehaviour
{
    public GameObject objectToFlash;
    public float ItemOnTime = 1f;
    public float ItemOffTime = 2;
    
    private float timeLeft1 ;
    private float timeLeft2;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft1 = ItemOnTime;
        timeLeft2 = ItemOffTime;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft1 -= Time.deltaTime;
        timeLeft2 -= Time.deltaTime;

        if (timeLeft1 < 0)
        {
            objectToFlash.SetActive(false);
            if (timeLeft2 < 0)
            {
                timeLeft1 = ItemOnTime;
                timeLeft2 = ItemOffTime;

            }


        }
        else
        {
            objectToFlash.SetActive(true);
        }

    }

}
