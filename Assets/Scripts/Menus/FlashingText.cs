using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingText : MonoBehaviour
{
    public float TimeToFlash = 1f;

    private Color textColorOn;
    private Color textColorOff;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        Color textColor = GetComponent<Text>().color;
        textColorOn = new Color(textColor.r, textColor.g, textColor.b, 1);
        textColorOff = new Color(textColor.r, textColor.g, textColor.b, 0);
        timer = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
