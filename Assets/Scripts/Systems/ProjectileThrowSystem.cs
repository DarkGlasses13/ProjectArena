using Leopotam.Ecs;
using UnityEngine;

public class ProjectileThrowSystem : IEcsRunSystem
{
    private EcsFilter<Bouncer, ThrowReady> _throwReadyBouncerFilter;

    public void Run()
    {
        foreach (int throwReadyBouncerIndex in _throwReadyBouncerFilter)
        {
            ref Bouncer bouncer = ref _throwReadyBouncerFilter.Get1(throwReadyBouncerIndex);
            ref ThrowReady throwReady = ref _throwReadyBouncerFilter.Get2(throwReadyBouncerIndex);
        }
    }
}
