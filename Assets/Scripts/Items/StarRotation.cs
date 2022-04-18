using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRotation : MonoBehaviour
{
    private float y;
    void Update()
    {
        y += 0.3f;
        y %= 360;
        transform.rotation = Quaternion.Euler(0, y, 0);
    }
}
