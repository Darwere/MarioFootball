using UnityEngine;
using UnityEngine.InputSystem;

public class InputBrain : PlayerBrain
{

    private Vector2 movementInput;

    private Vector3 direction;
    public override Vector3 MoveInput()
    {


        return new Vector3(-movementInput.y, 0, movementInput.x).normalized;
    }

    public override void Act()
    {
        actionMethods[action.type].DynamicInvoke();
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
        Field.Ball.Move(action.duration, action.startPosition, action.endPosition, action.bezierPoint);
        SwitchPlayer();

        action.type = PlayerAction.ActionType.None;
    }

    protected override void SwitchPlayer()
    {
        Player.IsPiloted = false; //last player piloted

        Player = action.target;
        Player.IsPiloted = true; //new player piloted

        action.type = PlayerAction.ActionType.None;
    }

    protected override void Shoot()
    {

        Field.Ball.Shoot(Enemies.ShootPoint,action.shootForce, action.direction, action.duration);


        action.type = PlayerAction.ActionType.None;
    }

    protected override void Tackle()
    {

        action.type = PlayerAction.ActionType.None;
        Debug.Log("Tackle");
    }

    protected override void Dribble()
    {

        action.type = PlayerAction.ActionType.None;
        Debug.Log("Drible");
    }

    protected override void Headbutt()
    {

        action.type = PlayerAction.ActionType.None;
        Debug.Log("Headbutt");
    }

    protected override void SendObject()
    {

        action.type = PlayerAction.ActionType.None;
        Debug.Log("SendObject");
    }

    #endregion

    #region InputEvent

    public void Movement(InputAction.CallbackContext input)
    {
        if (input.started) // We want to get the cancel event to change value to (0, 0, 0)
            return;

        Vector2 value = input.ReadValue<Vector2>();

        if (( (value.x < 0.1f && value.x > -0.1f) && (value.y < 0.1f && value.y > -0.1f) ))
        {
            value = new Vector2(0, 0);
            action.type = PlayerAction.ActionType.None;
        }
            

        value = value.normalized;
        direction = new Vector3(value.x, 0, value.y);


        PlayerAction act = PlayerAction.Move(direction);
        action = act;
    }

    public void Pass_SwitchPlayer(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;

        PlayerAction act;
        Vector3 startPos = Player.transform.position;
        Player targetPlayer = Allies.GetPlayerWithDirection(startPos, direction);

        if (Player.HasBall)
            act = PlayerAction.Pass(direction, startPos, targetPlayer.transform.position, 1f, targetPlayer); //Pass
        else
            act = PlayerAction.ChangePlayer(targetPlayer);  //SwitchPlayer

        action = act;
    }

    public void Shoot_Tackle(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;

        PlayerAction act;

        if (Player.HasBall)
            act = PlayerAction.Shoot(2f, direction, Player.transform.position, 2f); //Shoot
        else
        {
            Player targetPlayer = Enemies.GetPlayerWithDirection(Player.transform.position, direction);
            act = PlayerAction.Tackle(targetPlayer); //Tackle
        }    

        action = act;
    }

    public void Dribble_HeadButt(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;

        PlayerAction act;

        if (Player.HasBall)
            act = PlayerAction.Dribble(); //Dribble
        else
        {
            Player targetPlayer = Enemies.GetPlayerWithDirection(Player.transform.position,direction);
            act = PlayerAction.HeadButt(targetPlayer); //HeadButt
        }
            

        action = act;
    }

    public void SendObject(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;

        PlayerAction act = PlayerAction.Throw(direction);
        action = act;
    }

    #endregion
}
