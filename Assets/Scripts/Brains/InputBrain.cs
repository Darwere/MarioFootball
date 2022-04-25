using UnityEngine;
using UnityEngine.InputSystem;

public class InputBrain : PlayerBrain
{

    private Vector2 movementInput;

    private Vector3 direction;

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

        PlayerAction act = PlayerAction.Move(moveDirection);
        action = act;
    }

    public void Pass_SwitchPlayer(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;

        PlayerAction act = action;
        act.type = PlayerAction.ActionType.None; //Reset l'action

        Vector3 startPos = Player.transform.position;
        Player targetPlayer;

        if (Player.HasBall && (Player.CanMove || Player.IsKickOff))
        {
            targetPlayer = Allies.GetPlayerWithDirection(startPos, direction, 180f);
            act = PlayerAction.Pass(direction, Field.Ball.transform.position, targetPlayer.transform.position, targetPlayer); //Pass
        }
        else
        {
            targetPlayer = Allies.GetPlayerWithDirection(startPos, direction, 180f);
            act = PlayerAction.ChangePlayer(targetPlayer);  //SwitchPlayer
        }
            

        action = act;
    }

    public void Shoot_Tackle(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;

        PlayerAction act = action;
        act.type = PlayerAction.ActionType.None; //Reset l'action

        if (Player.CanMove)
        {
            if (Player.HasBall)
                act = PlayerAction.Shoot(2f, direction, Player.transform.position, 2f); //Shoot
            else
            {
                //Tackle
                Player targetPlayer = Enemies.GetPlayerWithDirection(Player.transform.position, direction, 20f);
                Vector3 vector;

                if (targetPlayer != null)
                    vector = targetPlayer.transform.position - Player.transform.position;
                else
                    vector = direction;

                act = PlayerAction.Tackle(vector.normalized);
            }
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
            Player targetPlayer = Enemies.GetPlayerWithDirection(Player.transform.position,direction, 20f);
            Vector3 vector;

            if (targetPlayer != null)
                vector = targetPlayer.transform.position - Player.transform.position;
            else
                vector = direction;

            act = PlayerAction.HeadButt(vector.normalized); //HeadButt
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

    public void Debugger(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;

        Debug.Log("PlayerName : " + Player.name + " State : " + Player.State);
    }
}
