using Leopotam.Ecs;
using UnityEngine;

public class CatchingSystem : IEcsRunSystem
{
    private EcsFilter<Projectile, Caught> _caughtProjectileFilter;
    private EcsFilter<CatchTrigger> _catchTriggerFilter;

    public void Run()
    {
        foreach (int index in _caughtProjectileFilter)
        {
            ref Projectile projectileComponent = ref _caughtProjectileFilter.Get1(index);
            ref Caught caught = ref _caughtProjectileFilter.Get2(index);
            ref Bouncer bouncerComponent = ref caught.Catcher.Get<Bouncer>();
        }

        foreach (int index in _catchTriggerFilter)
        {
            ref EcsEntity projectile = ref _catchTriggerFilter.GetEntity(index);
            ref Projectile projectileComponent = ref projectile.Get<Projectile>();
            ref Caught caught = ref projectile.Get<Caught>();
            ref Bouncer bouncerComponent = ref caught.Catcher.Get<Bouncer>();
            Vector3 catchPosition = new Vector3(0, 0, 1);

            projectileComponent.Vew.transform.SetParent(bouncerComponent.Vew.transform);
            projectileComponent.Vew.transform.localPosition = catchPosition;
            projectileComponent.Vew.SetActive(false);
            projectileComponent.Rigidbody.velocity = Vector3.zero;
            caught.Catcher.Del<Mover>();
        }
    }
}
