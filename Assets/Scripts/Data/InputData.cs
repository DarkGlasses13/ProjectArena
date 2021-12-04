using UnityEngine;
using Leopotam.Ecs;

public class InputData
{
    public JoysticSide JoysticSide;
    public float JoysticDeflection;

    public RaycastHit AimInfo;
    public bool IsAiming;

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
