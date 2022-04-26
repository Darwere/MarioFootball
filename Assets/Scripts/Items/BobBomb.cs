using UnityEngine;

public class BobBomb : Item
{
    public int force = 5;
    public int hauteur = 5;
    public float explosionArea = 15f;
    public GameObject explosionParticule;

    private float timeLeft = 2.5f;
    private Vector3 direction;
    private void Start()
    {
        ColliderOff(0.2f);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(direction*force);
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
            Explosion();
    }
    public override void Init(Player player)
    {
        direction = new Vector3(player.transform.forward.x * force, player.transform.forward.y + hauteur, player.transform.forward.z * force);
    }
    protected override void Move()
    {
        
    }

    private void Explosion()
    {
        Player[] team1Player = Field.Team1.Players;
        Player[] team2Player = Field.Team2.Players;
        for(int i = 0; i < team1Player.Length; i++)
        {
            ExplosionTest(team1Player[i]);
            ExplosionTest(team2Player[i]);
        }

        GameObject particule = Instantiate(explosionParticule, transform.position, Quaternion.identity);
        Destroy(particule, 1.5f);
        Destroy(this.gameObject);
    }

    private void ExplosionTest(Player player)
    {
        float sqrtDistance = (player.transform.position - transform.position).sqrMagnitude;

        if (sqrtDistance < explosionArea * explosionArea)
            player.GetTackled();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Explosion();
        }
    }
}
