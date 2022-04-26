using UnityEngine;

public class Mushroom : Item
{
    protected override void Move()
    {
    
    }

    public override void Init(Player Player)
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
