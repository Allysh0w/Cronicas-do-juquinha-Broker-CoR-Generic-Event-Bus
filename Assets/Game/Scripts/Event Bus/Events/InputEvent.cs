using UnityEngine;


public struct InputEvent 
{
    public InputEnum Type;
    public Vector2? MoveDirection;

    public InputEvent(InputEnum type, Vector2? moveDirection = null)
    {
        Type = type;
        MoveDirection = moveDirection;
    }
}
