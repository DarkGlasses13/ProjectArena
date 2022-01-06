using UnityEngine;
using Leopotam.Ecs;
using HellBounce;

public class SceneData : MonoBehaviour
{
    public EcsStartup Startup;

    [Header("OBJECTS")]
    public Camera Camera;
    public DynamicJoystick VirtualJoystic;

    [Header("[POINTS]")]
    public Transform Arena;
    public Transform Actors;
    public Transform PlayerSpawnPoint;
    public Transform[] Gates;
    public Transform ProjectileSpawnPoint;
    public Transform EnemyPool;

    [HideInInspector] public EcsEntity PlayerEntity;
}
