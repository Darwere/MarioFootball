using UnityEngine;

public class GoalBrain : PlayerBrain
{
    Ball ball;
    Vector3 goalPos;
    float goalRadius = 7.5f;
    float tolerance = 1f;
    public override Vector3 MoveInput()
    {
        return Vector3.zero;
    }

    protected override void Start()
    {
        base.Start();
        ball = Field.Ball;
        if (Player.Team == Field.Team1)
        {
            goalPos = (Field.TopLeftCorner + Field.BottomLeftCorner) / 2;
        }
        else if (Player.Team == Field.Team2)
        {
            goalPos = (Field.TopRightCorner + Field.BottomRightCorner) / 2;
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

    #region Control Player Methods

    protected override void Idle()
    {

    }

    protected override void Move()
    {

        Player.transform.position += action.direction * Time.deltaTime * Player.Species.speed;
    }

    protected override void Pass()
    {

    }

    protected override void SwitchPlayer()
    {

    }

    protected override void Shoot()
    {

    }

    protected override void Tackle()
    {

    }

    protected override void Dribble()
    {

    }

    protected override void Headbutt()
    {

    }

    protected override void SendObject()
    {

    }

    #endregion



    public override PlayerAction.ActionType Act()
    {
        actionMethods[action.type].DynamicInvoke();
        return action.type;
    }
}
