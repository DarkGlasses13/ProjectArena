using UnityEngine;
using UnityEngine.AI;
using Leopotam.Ecs;

public struct Enemy
{
    public NavMeshAgent NavMeshAgent;
    public EcsEntity Target;
    public float MoveSpeed;
}
