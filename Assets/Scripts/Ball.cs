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

    private void Awake()
    {
        rbBall = GetComponent<Rigidbody>();
        Vector3 nul1 = new Vector3(0,0.5f,0f);
        Vector3 nul2 = new Vector3(0f,0.5f,20f);
        Vector3 nul3 = new Vector3(15f,10f,0f);
        Move(1,nul1, nul2, nul3);
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
    }

    public Vector3 Bezier(Vector3 startingPoint, Vector3 destination, Vector3 bezierPoint, float duration)
    {
        bezierPercent += Time.deltaTime / duration;
        Vector3 resultBezier = Mathf.Pow(1 - bezierPercent, 2f) * startingPoint + 2 * (1 - bezierPercent) * bezierPercent * bezierPoint + Mathf.Pow(bezierPercent, 2f) * destination;

        if(bezierPercent > 1)
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
    /// Dessine une traînée derrière la balle
    /// </summary>
    private void DrawTrail()
    {

    }

    //A enleverrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr//
    public void Restart()
    {
        transform.position = new Vector3(0, 0.5f, 0);
    }

    public void ShootDirectLaDansLaLucarne(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        //Lalulu = ZoneAléatoire
        Vector3 LaLulu = new Vector3(-6.5f, 8.5f, -45f);
        if(LaLulu.x < 0)
        {
            minBezierX = LaLulu.x * 2;
            maxBezierX = -(LaLulu.x * 2);
        }
        else
        {
            minBezierX = -(LaLulu.x * 2);
            maxBezierX = LaLulu.x * 2;
        }
        Debug.Log(minBezierX);
        Debug.Log(maxBezierX);
        float randomBezier = Random.Range(minBezierX, maxBezierX);
        if (LaLulu.x < 0) 
        {
            while (randomBezier > LaLulu.x && randomBezier < -LaLulu.x)
            {
                randomBezier = Random.Range(minBezierX, maxBezierX);
            }
        }
        else
        {
            while (randomBezier < LaLulu.x && randomBezier > -LaLulu.x)
            {
                randomBezier = Random.Range(minBezierX, maxBezierX);
            }
        }
        Vector3 vecInterpolation = new Vector3(randomBezier, LaLulu.y + 4f, (LaLulu.z / 2 - 5));
        Move(1, startingPoint, LaLulu, vecInterpolation);
    }
}
