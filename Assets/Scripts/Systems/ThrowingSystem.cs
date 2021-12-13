using Leopotam.Ecs;
using UnityEngine;

public class ThrowingSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<ThrowTrigger> _throwFilter;

    public void Run()
    {
        foreach (int index in _throwFilter)
        {
            ref EcsEntity bouncer = ref _throwFilter.GetEntity(index);
            ref Vew bouncerVewComponent = ref bouncer.Get<Vew>();
            ref Bouncer bouncerComponent = ref bouncer.Get<Bouncer>();
            ref ThrowReady throwReadyComponent = ref bouncer.Get<ThrowReady>();

            ref EcsEntity projectile = ref throwReadyComponent.ThrowableProjectile;
            ref Projectile projectileComponent = ref projectile.Get<Projectile>();
            ref Vew projectileVewComponent = ref projectile.Get<Vew>();

            projectileVewComponent.Object.transform.SetParent(_sceneData.Arena);
            projectileVewComponent.Object.SetActive(true);

            throwReadyComponent.ThrowableProjectile.Del<Caught>();
            bouncer.Del<ThrowReady>();
            bouncer.Del<Aiming>();
            _sceneData.Startup.SetUpdateSystemState(SystemName.Aim, false);
            _sceneData.Startup.SetFixedUpdateSystemState(SystemName.Move, true);
            _sceneData.Joystick.enabled = true;
        }
    }
}
