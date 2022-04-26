using UnityEngine;

public class GreenTurtleShell : Item
{
    public GameObject destructionParticule;

    [SerializeField]
    private float speed = 10f;

    private int countWallHit = 0;
    private Vector3 direction;

    private void Start()
    {
        ColliderOff(0.2f);
    }
    private void Update()
    {
        Move();
        if(countWallHit == 3)
        {
            direction = Vector3.zero;
            GameObject particule = Instantiate(destructionParticule, transform.position, Quaternion.identity);
            Destroy(particule, 0.3f);
            Destroy(this.gameObject, 0.1f);
        }
    }
    protected override void Move()
    {
        transform.position += direction*Time.deltaTime*speed;
    }

    public override void Init(Player player)
    {
        direction = player.transform.forward;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        Player player = collision.gameObject.GetComponent<Player>();
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (player != null)
        {
            player.GetTackled();
            //Rajouter particules eclatement
            direction = Vector3.zero;
            GameObject particule = Instantiate(destructionParticule, transform.position, Quaternion.identity);
            //Destroy(particule, 0.3f);
            Destroy(this.gameObject,0.1f);
        }
        else if(ball!=null)
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
