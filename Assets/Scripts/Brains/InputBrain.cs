using UnityEngine;
using UnityEngine.InputSystem;

public class InputBrain : PlayerBrain
{

    private Vector2 movementInput;

    public override Vector3 Move()
    {


        return new Vector3(-movementInput.y, 0, movementInput.x).normalized;
    }

    public void Sprint(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;

        Vector2 value = input.ReadValue<Vector2>().normalized;
        Vector3 direction = new Vector3(value.x, 0, value.y);

        Action act = Action.Move(direction);
        act = Action.Sprint();
        action.isSprinting = act.isSprinting;
    }

    public void Movement(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;
        Vector2 value = input.ReadValue<Vector2>().normalized;
        Vector3 direction = new Vector3(value.x, 0, value.y);

        Action act = Action.Move(direction);
        action = act;
    }

    public void Pass(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;


    }

    public void Shoot_Tacle(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;

    }

    public void Dribble_HeadButt(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;

    }

    public void SendObject(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;

    }

    public override Action Act()
    { 
        return new Action();
    }
}
