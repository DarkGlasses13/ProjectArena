using UnityEngine;
using Leopotam.Ecs;

public class InputData
{
    public Vector2 MotionInput;

    public RaycastHit ClickInfo;

    public Touch Touch
    {
        get
        {
            return Input.GetTouch(0);
        }
    }
    public bool IsTouching
    {
        get
        {
            return Input.touchCount > 0;
        }
    }
}
