using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    private Vector3 startingPoint;
    private Vector3 destination;
    private Vector3 bezierPoint;
    private float force;
    private bool trail;
    private bool isFree;
    private Color trailColorBegin;
    private Color trailColorEnd;
    private Rigidbody rbBall;

    private float duration;
    private float bezierPercent;
    private bool isMovable = false;
    private float minBezierZ;
    private float maxBezierZ;
    private Vector3 offset = new Vector3 (0,0.5f,0);
    private Player player = new Player();

    private void Awake()
    {
        rbBall = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (isMovable)
        {
            transform.position = Bezier(startingPoint, destination, bezierPoint, duration);
        }
    }

    public void Move(float duration, Vector3 startingPoint, Vector3 destination, Vector3 bezierPoint)
    {
        isMovable = true;
        bezierPercent = 0;
        this.duration = duration;
        this.startingPoint = startingPoint + offset;
        this.destination = destination+ offset;
        this.bezierPoint = bezierPoint;
        DetachFromParent();
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
        Vector3 vecInterpolation = new Vector3((posCage.position.x / 2 - 5), posCage.position.y + 4f, randomBezier);
        Move(1, transform.position, posCage.position, vecInterpolation);
        DetachFromParent();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        Debug.Log("Pas de passe a soi-meme !");
        if (player != null && this.player != player)
        {
            Debug.Log("Récupération de balle");
            AttachToPlayer(player);
        }

    }

    private void AttachToPlayer(Player parent)
    {
        player = parent;
        isMovable = false;
        parent.GetBall(this);
        transform.parent = parent.transform;
        transform.position += parent.transform.forward*1;
        ResetVelocity();
        //isFree = false;
    }

    private void DetachFromParent()
    {
        transform.parent = null;
        //isFree = true;
        //isMovable = true;
    }
}
