using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBobBombe : MonoBehaviour
{
    private float x;
    void Update()
    {
        x+=0.002f;
        x %= 360;
        transform.Rotate(0, x, 0, Space.Self);
    }


}
