using UnityEngine;

public struct MoveButton
{
    public GameObject Button;
    public MoveSide MoveSide;
    public bool IsClicked;
}

public enum MoveSide
{
    Left,
    Right
}
