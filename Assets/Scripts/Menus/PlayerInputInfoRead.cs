using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputInfoRead : MonoBehaviour
{
    [SerializeField] PlayerInput pinput1;
    [SerializeField] List<string> dev = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dev = new List<string>();
        foreach (InputDevice ID in pinput1.devices)
        {
            dev.Add(ID.name);
        }

    }
}
