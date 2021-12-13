using Leopotam.Ecs;
using UnityEngine;

public class ProjectileTrackingSystem : IEcsRunSystem
{
    private EcsFilter<Projectile, Vew>.Exclude<Caught> _projectileFilter;
    private EcsFilter<Bouncer, Vew>.Exclude<ThrowReady> _movableBouncerFilter;

    public void Run()
    {
        foreach (int p in _projectileFilter)
        {
            foreach (int b in _movableBouncerFilter)
            {
                ref Vew projectileVewComponent = ref _projectileFilter.Get2(p);
                ref Vew bouncerVewComponent = ref _movableBouncerFilter.Get2(b);

                bouncerVewComponent.Object.transform.LookAt(projectileVewComponent.Object.transform);
            }
        }
    }
}
