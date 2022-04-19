using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Banana : Item
{
    public Sprite sprite;
    public int force = 5;
    public int hauteur = 5;
    private void Start()
    {
        ColliderOff(0.4f);
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 direction = new Vector3(transform.forward.x * force, transform.forward.y + hauteur, transform.forward.z * force);
        rb.AddForce(direction * force);
    }
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
