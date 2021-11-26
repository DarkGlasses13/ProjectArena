using UnityEngine;

public class SceneData : MonoBehaviour
{
    public Camera Camera;
    public Transform Arena;
    public Transform LeftMovableEdge;
    public Transform RightMovableEdge;
    public GameObject LeftMoveButton;
    public GameObject RightMoveButton;

    [HideInInspector] public Transform Bouncer;

    public Vector3 MovableCenter
    {
        get
        {
            return (LeftMovableEdge.position + RightMovableEdge.position) / 2;
        }
    }
}
