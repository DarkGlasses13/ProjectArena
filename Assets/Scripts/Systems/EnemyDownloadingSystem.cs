using UnityEngine;
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

            float distance = Vector3.Distance
            (
                vewComponent.Object.transform.position,
                enemyComponent.Target.Get<Vew>().Object.transform.position
            );

            if (distance <= enemyComponent.NavMeshAgent.stoppingDistance)
            {
                vewComponent.Animator.SetInteger(Animations.EnemyStates.State, Animations.EnemyStates.DownloadState);
            }
        }
    }
}