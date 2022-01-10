using UnityEngine;
using UnityEngine.AI;
using Leopotam.Ecs;

sealed class DownloadingSystem : IEcsRunSystem
{
    private EcsFilter<Vew, Enemy, Downloader> _downloaderFilter;
    public void Run()
    {
        foreach (int index in _downloaderFilter)
        {
            ref Vew vewComponent = ref _downloaderFilter.Get1(index);
            ref Enemy enemyComponent = ref _downloaderFilter.Get2(index);

            if (enemyComponent.NavMeshAgent.remainingDistance <= enemyComponent.NavMeshAgent.stoppingDistance)
            {
                vewComponent.Animator.SetInteger(Animations.EnemyStates.State, Animations.EnemyStates.DownloadState);
            }
        }
    }
}