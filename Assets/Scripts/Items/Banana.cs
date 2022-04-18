using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : Item
{
    protected override void Move()
    {
        
    }

    public override void Init(Team team)
    {

    }

    protected override void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.GetTackled();
            Destroy(this.gameObject);
        }
    }
}
