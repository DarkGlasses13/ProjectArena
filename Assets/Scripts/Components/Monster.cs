using UnityEngine;
using UnityEngine.AI;
using Leopotam.Ecs;

public struct Monster
{
    public NavMeshAgent NavMeshAgent;
    public EcsEntity Target;
    public float MoveSpeed;
}
