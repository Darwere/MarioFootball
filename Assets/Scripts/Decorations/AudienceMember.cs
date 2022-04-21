using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceMember : MonoBehaviour
{
    Animator anim;

    public bool Cheering = false;
    public bool Break = false;
    public bool Clap = false;

    private bool _cheer;
    private bool _break;
    private bool _clap;
    private float clapChance = 2000;

    public bool isIdle;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        isIdle = anim.GetCurrentAnimatorStateInfo(0).IsTag("Idle");
        _cheer = anim.GetCurrentAnimatorStateInfo(0).IsTag("Cheer");
        _break = anim.GetCurrentAnimatorStateInfo(0).IsTag("Break");
        _clap = anim.GetCurrentAnimatorStateInfo(0).IsTag("Clap");

        clapChance = (_clap)? 500 : 2000;
        if (Random.Range(0, clapChance) == 1) Clap = !Clap;

        anim.SetBool("Clap", Clap);
        if(!(Clap||_cheer||_break))
        {
            int rand = Random.Range(0, 1000);
            if (rand == 1) Break = true;
            if (rand == 2) Cheering = true;



            if (Break)
            {
                Break = false;
                anim.SetTrigger("Break");
            }

            if (Cheering)
            {
                Cheering = false;
                anim.SetTrigger("Cheer");
            }
        }
        
    }
}
