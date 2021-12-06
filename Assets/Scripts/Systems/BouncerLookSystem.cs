using Leopotam.Ecs;
using UnityEngine;

public class BouncerLookSystem : IEcsRunSystem
{
    private Configuration _configuration;
    private EcsFilter<Projectile> _projectileFilter;
    private EcsFilter<Mover, Bouncer> _movableBouncerFilter;

    public void Run()
    {
        foreach (int projectileIndex in _projectileFilter)
        {
            foreach (int bouncerIndex in _movableBouncerFilter)
            {
                ref Projectile projectileComponent = ref _projectileFilter.Get1(projectileIndex);
                ref Bouncer bouncerComponent = ref _movableBouncerFilter.Get2(bouncerIndex);

                if (projectileComponent.Vew.transform.position.z > bouncerComponent.Vew.transform.position.z)
                {
                    bouncerComponent.Vew.transform.rotation = LookAtProjectile(bouncerComponent.Vew.transform.rotation, Vector3.forward);
                }
                else
                {
                    bouncerComponent.Vew.transform.rotation = LookAtProjectile(bouncerComponent.Vew.transform.rotation, Vector3.back);
                }
            }
        }
    }

    private Quaternion LookAtProjectile(Quaternion current, Vector3 direction)
    {
        return Quaternion.Slerp
        (
            current,
            Quaternion.LookRotation(direction),
            _configuration.RotationSmooth * Time.deltaTime
        );
    }
}
