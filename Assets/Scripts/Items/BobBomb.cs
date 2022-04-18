using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobBomb : Item
{
    public int force = 5;
    public int hauteur = 5;
    public GameObject explosionParticule;
    public GameObject explosionArea;

    private float timeLeft = 2.5f;
    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 direction = new Vector3(transform.forward.x*force, transform.forward.y + hauteur, transform.forward.z*force);
        rb.AddForce(direction*force);
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            Instantiate(explosionArea, transform.position, Quaternion.identity);
            GameObject particule = Instantiate(explosionParticule, transform.position, Quaternion.identity);
            Destroy(particule, 1.5f);
            Destroy(this.gameObject);
        }
    }
    public override void Init(Team team)
    {

    }
    protected override void Move()
    {
        
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            GameObject explosion = Instantiate(explosionArea, transform.position, Quaternion.identity);
            GameObject particule = Instantiate(explosionParticule, transform.position, Quaternion.identity);
            Destroy(particule, 1.5f);
            Destroy(this.gameObject);
        }
    }
}
