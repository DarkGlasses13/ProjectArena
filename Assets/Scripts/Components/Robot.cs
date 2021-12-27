using UnityEngine;
using UnityEngine.AI;
using Leopotam.Ecs;

public struct Robot
{
    public NavMeshAgent NavMeshAgent;
    public EcsEntity Target;
    public float MoveSpeed;
}
