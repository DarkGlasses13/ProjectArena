using UnityEngine;

public class SceneData : MonoBehaviour
{
    [Header("OBJECTS")]
    public Camera Camera;
    public GameObject MovePanel;
    public Transform Arena;
    public Transform[] Generators;

    [Header("[POINTS]")]
    public Transform LeftMovableEdge;
    public Transform RightMovableEdge;
    public Transform ProjectileSpawner;
    public Transform LeftMonsterGates;
    public Transform RightMonsterGates;

    [HideInInspector] public GameObject Player;

    public Vector3 MovableCenter
    {
        get
        {
            return (LeftMovableEdge.position + RightMovableEdge.position) / 2;
        }
    }
}
