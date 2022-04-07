using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBobBombe : MonoBehaviour
{
    private int x;
    void Update()
    {
        x++;
        x %= 360;
        transform.rotation = Quaternion.Euler(x, -90, 90);
    }


}
