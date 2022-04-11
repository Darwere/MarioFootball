using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperStar : Item
{
    protected override void Move()
    {

    }


    protected override void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("etoile");
            Destroy(this.gameObject);
        }
    }
}
