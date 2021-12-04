using UnityEngine;

public class SceneData : MonoBehaviour
{
    public Camera Camera;
    public GameObject MovePanel;
    public Transform Arena;
    public Transform LeftMovableEdge;
    public Transform RightMovableEdge;
    public Transform ProjectileSpawner;

    [HideInInspector] public GameObject Player;

    public Vector3 MovableCenter
    {
        get
        {
            return (LeftMovableEdge.position + RightMovableEdge.position) / 2;
        }
    }
}
