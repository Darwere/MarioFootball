using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animtester : MonoBehaviour
{
    AnimatorOverrideController over;
    Animator reg;
    [SerializeField] bool moving = false;
    [SerializeField] bool pass = false;
    // Start is called before the first frame update
    void Start()
    {
        over = GetComponent<AnimatorOverrideController>();
        reg = GetComponent<Animator>();

        print(reg.parameterCount);
    }

    // Update is called once per frame
    void Update()
    {
        reg.SetBool("Moving", moving);
        if(pass)
        {
            reg.SetTrigger("Pass");
            pass = false;
        }

    }
}
