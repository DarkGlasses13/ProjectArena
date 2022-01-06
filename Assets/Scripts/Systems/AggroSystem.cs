using Leopotam.Ecs;
using UnityEngine;

public class AggroSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<Vew, Server>.Exclude<Busy> _freeServerFilter;
    private EcsFilter<Vew, Enemy, Awakened> _awakenedEnemyFilter;

    public void Run()
    {
        foreach (int index in _awakenedEnemyFilter)
        {
            ref EcsEntity enemy = ref _awakenedEnemyFilter.GetEntity(index);
            ref Enemy enemyComponent = ref _awakenedEnemyFilter.Get2(index);
            ref Vew vewComponent = ref _awakenedEnemyFilter.Get1(index);

            if (enemyComponent.Target == EcsEntity.Null)
            {
                enemyComponent.Target = GetTarget(enemy, out float stoppingDistance);
                enemyComponent.NavMeshAgent.stoppingDistance = stoppingDistance;
            }

            enemyComponent.NavMeshAgent.SetDestination(enemyComponent.Target.Get<Vew>().Object.transform.position);
            vewComponent.Animator.SetInteger(Animations.EnemyStates.State, Animations.EnemyStates.MoveState);
        }
    }

    private EcsEntity GetTarget(EcsEntity enemy, out float stoppingDistance)
    {
        float stoppingDistanceForPlayer = 2;
        float stoppingDistanceForServer = 1;

        foreach (int index in _freeServerFilter)
        {
            ref EcsEntity server = ref _freeServerFilter.GetEntity(index);
            ref Server serverComponent = ref _freeServerFilter.Get2(index);

            server.Get<Busy>();
            serverComponent.Desstroyer = enemy;
            enemy.Get<Downloader>();
            stoppingDistance = stoppingDistanceForServer;
            return server;
        }

        enemy.Get<PlayerAttacker>();
        stoppingDistance = stoppingDistanceForPlayer;
        return _sceneData.PlayerEntity;
    }
}
