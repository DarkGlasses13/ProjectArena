using UnityEngine;
using Leopotam.Ecs;

sealed class EnemyAttackSystem : IEcsRunSystem
{
    private EcsFilter<Vew, Enemy, PlayerAttacker> _playerAttackerFilter;

    public void Run()
    {
        foreach (int index in _playerAttackerFilter)
        {
            ref Vew vewComponent = ref _playerAttackerFilter.Get1(index);
            ref Enemy enemyComponent = ref _playerAttackerFilter.Get2(index);

            float distance = Vector3.Distance
            (
                vewComponent.Object.transform.position,
                enemyComponent.Target.Get<Vew>().Object.transform.position
            );

            if (distance <= enemyComponent.NavMeshAgent.stoppingDistance)
            {
                vewComponent.Animator.SetInteger(Animations.EnemyStates.State, Animations.EnemyStates.AttackState);
            }
        }
    }
}