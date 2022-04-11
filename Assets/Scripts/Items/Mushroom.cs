using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Item
{
    protected override void Move()
    {
    
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Destroy(this.gameObject);
        }
    }
}
