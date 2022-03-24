using UnityEngine;
using UnityEngine.InputSystem;

public class InputBrain : PlayerBrain
{

    private Vector2 movementInput;

    private Vector3 direction;

    private void Update()
    {
        
    }

    public override Vector3 MoveInput()
    {


        return new Vector3(-movementInput.y, 0, movementInput.x).normalized;
    }

    public override PlayerAction.ActionType Act()
    {
        PlayerAction.ActionType lastActionType = action.type;

        actionMethods[action.type].DynamicInvoke();

        if(action.type != PlayerAction.ActionType.Move)
            action.type = PlayerAction.ActionType.None; //Reset l'action
        return lastActionType;
    }

    #region Control Player Methods

    protected override void Idle()
    {

    }

    protected override void Move()
    {
        Player.transform.position += action.direction * Time.deltaTime * Player.Species.speed;

        if(action.direction != Vector3.zero)
        {
            Player.transform.forward = action.direction;
        }
    }

    protected override void Pass()
    {
        Field.Ball.Move(action.duration, action.startPosition, action.endPosition, action.bezierPoint);
        Player.GetBall(null);
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

        Player.GetBall(null);
        action.type = PlayerAction.ActionType.None;
    }

    protected override void Tackle()
    {
        //action.type = PlayerAction.ActionType.None;
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
        Vector3 moveDirection;

        if (( (value.x < 0.1f && value.x > -0.1f) && (value.y < 0.1f && value.y > -0.1f) ))
        {
            value = new Vector2(0, 0);
            action.type = PlayerAction.ActionType.None;
            moveDirection = new Vector3(value.x, 0, value.y);
        }  
        else
        {
            value = value.normalized;
            direction = new Vector3(value.x, 0, value.y);
            moveDirection = direction;
        }

        if(Player.CanMove)
        {
            PlayerAction act = PlayerAction.Move(moveDirection);
            action = act;
        }
        
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
        //Debug.Log("Shoot direction : " + direction);
        if (Player.HasBall)
            act = PlayerAction.Shoot(2f, direction, Player.transform.position, 2f); //Shoot
        else
        {
            Player targetPlayer = Enemies.GetPlayerWithDirection(Player.transform.position, direction);
            act = PlayerAction.Tackle(targetPlayer); //Tackle
            //Debug.Log("PlayerTarget transform : " + targetPlayer.transform.position);
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
