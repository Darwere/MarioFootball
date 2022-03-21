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
    private float minBezierX;
    private float maxBezierX;

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

    public void ShootInput(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        Shoot(DebugTab);
    }

    public void Shoot(Vector3[] randomPos)
    {
        int posCageIndex = Random.Range(0, (randomPos.Length));
        Vector3 posCage = randomPos[posCageIndex];
        if (posCage.x < 0)
        {
            minBezierX = posCage.x * 2;
            maxBezierX = -(posCage.x * 2);
        }
        else
        {
            minBezierX = -(posCage.x * 2);
            maxBezierX = posCage.x * 2;
        }
        float randomBezier = Random.Range(minBezierX, maxBezierX);
        if (posCage.x < 0)
        {
            while (randomBezier > posCage.x && randomBezier < -posCage.x)
            {
                randomBezier = Random.Range(minBezierX, maxBezierX);
            }
        }
        else
        {
            while (randomBezier < posCage.x && randomBezier > -posCage.x)
            {
                randomBezier = Random.Range(minBezierX, maxBezierX);
            }
        }
        Vector3 vecInterpolation = new Vector3(randomBezier, posCage.y + 4f, (posCage.z / 2 - 5));
        Move(1, startingPoint, posCage, vecInterpolation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            AttachToPlayer(player.transform);
        }
    }

    private void AttachToPlayer(Transform parent)
    {
        transform.parent = parent;
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
