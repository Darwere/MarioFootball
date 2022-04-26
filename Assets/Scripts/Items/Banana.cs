using UnityEngine;

public class Banana : Item
{
    public int force = 5;
    public int hauteur = 5;
    private Vector3 direction;
    private void Start()
    {
        ColliderOff(0.4f);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(direction * force);
    }
    protected override void Move()
    {
        
    }

    public override void Init(Player player)
    {
        direction = new Vector3(player.transform.forward.x * force, player.transform.forward.y + hauteur, player.transform.forward.z * force);
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
