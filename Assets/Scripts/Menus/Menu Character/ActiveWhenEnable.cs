using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWhenEnable : MonoBehaviour
{
    public GameObject ObjectToActive;

    private void OnEnable()
    {
        ObjectToActive.SetActive(true);
    }
}
