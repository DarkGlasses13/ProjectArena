using Leopotam.Ecs;
using UnityEngine;

sealed class ProjectileAccelerationSystem : IEcsRunSystem
{
    private ConfigData _configData;
    private EcsFilter<Projectile, Vew>.Exclude<Caught> _projectileFilter;

    public void Run()
    {
        foreach (int index in _projectileFilter)
        {
            ref Projectile projectileComponent = ref _projectileFilter.Get1(index);
            ref Vew vew = ref _projectileFilter.Get2(index);

            vew.Object.transform.Translate(Vector3.forward * projectileComponent.Speed * Time.deltaTime);
        }
    }
}