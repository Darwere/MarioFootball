using UnityEngine;

public class GoalBrain : PlayerBrain
{
    Ball ball;
    Vector3 goalPos;
    float goalRadius = 7.5f;
    float tolerance = 1f;

    protected void Start()
    {
        ball = Field.Ball;
        if (Player.Team == Field.Team1)
        {
            goalPos = (Field.TopRightCorner + Field.BottomRightCorner) / 2;
        }
        else if (Player.Team == Field.Team2)
        {
            goalPos = (Field.TopLeftCorner + Field.BottomLeftCorner) / 2;
        }
        else
        {
            Debug.Log("Goal ni team 1 ni team 2 wtf");
        }
    }

    protected void Update()
    {
        Vector3 ballToGoal = ball.transform.position - goalPos;
        Vector3 posCircle = goalPos + goalRadius * ballToGoal.normalized;
        if (Vector3.Distance(posCircle, Player.transform.position) > tolerance)
        {
            Vector3 toPosCircle = posCircle - Player.transform.position;
            toPosCircle = new Vector3 (toPosCircle.x, 0, toPosCircle.z);
            Vector3 direction = toPosCircle.normalized;
            action = PlayerAction.Move(direction);
        }
        else { action = PlayerAction.Move(Vector3.zero); }
    }
}
