using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTurtleShell : Item
{
    public GameObject iceBlock;
    public GameObject destructionParticule;

    [SerializeField]
    private float speed = 10f;

    private int countWallHit = 0;
    private Vector3 direction;

    private void Start()
    {
        ColliderOff(0.2f);
        direction = transform.forward;
    }
    private void Update()
    {
        Move();
        if (countWallHit == 3)
        {
            direction = Vector3.zero;
            GameObject particule = Instantiate(destructionParticule, transform.position, Quaternion.identity);
            Destroy(particule, 0.3f);
            Destroy(this.gameObject, 0.1f);
        }
    }
    protected override void Move()
    {
        transform.position += direction * Time.deltaTime * speed;
    }

    public override void Init(Team team)
    {

    }

    protected override void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (player != null)
        {
            player.GetFreeze();
            direction = Vector3.zero;
            GameObject ice = Instantiate(iceBlock, player.transform.position, Quaternion.identity);
            ice.GetComponent<IceBlock>().Init(player);
            Destroy(this.gameObject, 0.1f);
        }
        else if (ball != null)
        {

        }
        else
        {
            direction = Vector3.Reflect(direction, collision.contacts[0].normal);
            transform.LookAt(direction);
            countWallHit++;
        }
    }
}
