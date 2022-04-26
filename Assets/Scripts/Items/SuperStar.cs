using UnityEngine;

public class SuperStar : Item
{
    protected override void Move()
    {

    }

    public override void Init(Player player)
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
