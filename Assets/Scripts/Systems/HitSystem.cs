using Leopotam.Ecs;
using UnityEngine;

public class HitSystem : IEcsRunSystem
{
    private EcsFilter<Robot, HitTrigger> _hitedMonsterFilter;
    private EcsFilter<Generator, HitTrigger> _hitedGeneratorFilter;

    public void Run()
    {
        foreach (int index in _hitedMonsterFilter)
        {
            _hitedMonsterFilter.GetEntity(index).Get<Dead>();
        }

        foreach (int index in _hitedGeneratorFilter)
        {
            _hitedGeneratorFilter.Get1(index).Helth--;
        }
    }
}
