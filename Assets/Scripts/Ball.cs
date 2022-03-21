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

    private Vector3 aV = new Vector3(-6.5f, 8.5f, -45f);
    private Vector3 bV = new Vector3(0, 8.5f, -45);
    private Vector3 cV = new Vector3(6.5f, 8.5f, -45);
    private Vector3 dV = new Vector3(-6.5f, 1, -45);
    private Vector3 eV = new Vector3(6.5f, 1, -45);
    private Vector3 fV = new Vector3(0, 1, -45);
    private Vector3[] DebugTab;


    private void Awake()
    {
        rbBall = GetComponent<Rigidbody>();
        Vector3 nul1 = new Vector3(0, 0.5f, 0f);
        Vector3 nul2 = new Vector3(0f, 0.5f, 20f);
        Vector3 nul3 = new Vector3(15f, 10f, 0f);
        //Move(1,nul1, nul2, nul3);
        DebugTab = new Vector3[] { aV, bV, cV, dV, eV, fV };
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
        this.startingPoint = startingPoint;
        this.destination = destination;
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
        transform.position = new Vector3(0, 0.5f, 0);
    }

    public void Shoot(Transform[] randomPos, float force, Vector3 direction, float duration)
    {
        int posCageIndex = Random.Range(0, (randomPos.Length));
        Transform posCage = randomPos[posCageIndex];
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
        if (player != null)
        {
            AttachToPlayer(player);
        }
    }

    private void AttachToPlayer(Player parent)
    {
        parent.GetBall(this);
        transform.parent = parent.transform;
        transform.position += parent.transform.forward*1;
        ResetVelocity();
        //isMovable = false;
        //isFree = false;
    }

    private void DetachFromParent()
    {
        transform.parent = null;
        //isFree = true;
        //isMovable = true;
    }
}
