using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool GotShooted  { get; private set; }
    public Vector3 ShootPoint { get; private set; }

    public Vector3 StartingPoint { get; private set; }
    public Vector3 Destination { get; private set; }
    public Vector3 BezierPoint { get; private set; }

    public bool InGoal = false;
    private bool isFree = true;
    private Rigidbody rbBall;

    public float Duration { get; private set; }
    private float bezierPercent;
    private bool isMovable = false;
    private float minBezierZ;
    private float maxBezierZ;
    private Vector3 offset = new Vector3 (0,0.5f,0);
    private Player player = new Player();

    private float angle = 60f;

    private void Awake()
    {
        rbBall = GetComponent<Rigidbody>();
        GotShooted = false;
    }
    private void Update()
    {
        if (isMovable)
        {
            transform.position = Bezier(StartingPoint, Destination, BezierPoint, Duration);
        }
    }

    public void Move(float duration, Vector3 startingPoint, Vector3 destination, Vector3 bezierPoint)
    {
        isMovable = true;
        bezierPercent = 0;
        this.Duration = duration;
        this.StartingPoint = startingPoint + offset;
        this.Destination = destination+ offset;
        this.BezierPoint = bezierPoint;
        DetachFromParent();
    }

    private Vector3 BallisticVelocity(Vector3 destination, float angle)
    {
        Vector3 dir = destination - transform.position; // get Target Direction
        float height = dir.y; // get height difference
        dir.y = 0; // retain only the horizontal difference
        float dist = dir.magnitude; // get horizontal direction
        float a = angle * Mathf.Deg2Rad; // Convert angle to radians
        dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
        dist += height / Mathf.Tan(a); // Correction for small height differences

        // Calculate the velocity magnitude
        float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * dir.normalized; // Return a normalized vector.
    }

    public Vector3 Bezier(Vector3 startingPoint, Vector3 destination, Vector3 bezierPoint, float duration)
    {
        bezierPercent += Time.deltaTime / duration;
        Vector3 resultBezier = Mathf.Pow(1 - bezierPercent, 2f) * startingPoint + 2 * (1 - bezierPercent) * bezierPercent * bezierPoint + Mathf.Pow(bezierPercent, 2f) * destination;

        if (bezierPercent > 1)
        {
            isMovable = false;
        }
        return resultBezier;
    }

    private void ResetVelocity()
    {
        rbBall.velocity = Vector3.zero;

    }

    /// <summary>
    /// Dessine une tra�n�e derri�re la balle
    /// </summary>
    private void DrawTrail()
    {

    }

    //A enleverrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr//
    public void Restart()
    {
        GotShooted = false;
        isMovable = false;
        transform.position = new Vector3(0, 0.6f, 0);
    }

    public void Shoot(Transform[] shootPos, float force, Vector3 direction, float duration)
    {
        int posCageIndex = Random.Range(0, (shootPos.Length));
        Transform posCage = shootPos[posCageIndex];

        if (posCage.position.z < 0)
        {
            minBezierZ = posCage.position.z * 2;
            maxBezierZ = -(posCage.position.z * 2);
        }
        else
        {
            minBezierZ = -(posCage.position.z * 2);
            maxBezierZ = posCage.position.z * 2;
        }

        float randomBezier = Random.Range(minBezierZ, maxBezierZ);

        if (posCage.position.z < 0)
        {
            while (randomBezier > posCage.position.z && randomBezier < -posCage.position.z)
            {
                randomBezier = Random.Range(minBezierZ, maxBezierZ);
            }
        }
        else
        {
            while (randomBezier < posCage.position.z && randomBezier > -posCage.position.z)
            {
                randomBezier = Random.Range(minBezierZ, maxBezierZ);
            }
        }

        Vector3 vecInterpolation = new Vector3((posCage.position.x), posCage.position.y + 4f, randomBezier);
        Move(2, transform.position, posCage.position, vecInterpolation);

        DetachFromParent();

        ShootPoint = posCage.position;
        GotShooted = true;
    }

    public void BadShoot(Transform[] shootPos, Vector3  direction)
    {
        int posCageIndex = Random.Range(0, (shootPos.Length));
        Transform posCage = shootPos[posCageIndex];
        direction = posCage.position - transform.position;
        DetachFromParent();
        var velocity = BallisticVelocity(direction, angle)/2;
        rbBall.velocity = velocity;
    }

    public void AttachToPlayer(Player parent)
    {
        player = parent;
        isMovable = false;
        transform.parent = parent.transform;
        transform.position = parent.transform.position + parent.transform.forward;// + Vector3.up * transform.position.y;
        isFree = false;
        GotShooted = false;
        InGoal = false;
        ResetVelocity();
        player.Team.ChangePilotedPlayer(player);

        rbBall.isKinematic = true;
        //isFree = false;
    }

    public void DetachFromParent()
    {
        transform.parent = null;
        rbBall.isKinematic = false;
        isFree = true;
        //isMovable = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null && this.player != player)
        {
            if (isFree)
                AttachToPlayer(player);
        }
    }
}
