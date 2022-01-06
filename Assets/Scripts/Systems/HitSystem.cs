using Leopotam.Ecs;
using UnityEngine;

public class HitSystem : IEcsRunSystem
{
    private EcsFilter<Enemy, HitTrigger> _hitedMonsterFilter;
    private EcsFilter<Server, HitTrigger> _hitedGeneratorFilter;

    public void Run()
    {
        foreach (int index in _hitedMonsterFilter)
        {
            _hitedMonsterFilter.GetEntity(index).Get<DeadTrigger>();
        }

        foreach (int index in _hitedGeneratorFilter)
        {
            _hitedGeneratorFilter.Get1(index).DataCount--;
        }
    }
}
