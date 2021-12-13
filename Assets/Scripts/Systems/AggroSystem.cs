using Leopotam.Ecs;
using UnityEngine;

public class AggroSystem : IEcsRunSystem
{
    private EcsFilter<Monster, Aggressive>.Exclude<Breaking> _aggressiveMonsterFilter;

    public void Run()
    {
        foreach (int index in _aggressiveMonsterFilter)
        {
            ref EcsEntity monster = ref _aggressiveMonsterFilter.GetEntity(index);
            ref Vew vewComponent = ref monster.Get<Vew>();
            ref Monster monsterComponent = ref monster.Get<Monster>();

            if (monsterComponent.Target != EcsEntity.Null)
            {
                monsterComponent.NavMeshAgent.SetDestination(monsterComponent.Target.Get<Vew>().Object.transform.position);
            }
        }
    }
}
