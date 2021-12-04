using Leopotam.Ecs;
using UnityEngine;

public class ThrowingSystem : IEcsRunSystem
{
    private Configuration _configuration;
    private SceneData _sceneData;
    private InputData _inputData;
    private EcsFilter<Bouncer, ThrowReady, ThrowTrigger> _threwBouncerFilter;

    public void Run()
    {
        foreach (int index in _threwBouncerFilter)
        {
            ref EcsEntity bouncer = ref _threwBouncerFilter.GetEntity(index);
            ref Bouncer bouncerComponent = ref _threwBouncerFilter.Get1(index);
            ref ThrowReady throwReadyComponent = ref _threwBouncerFilter.Get2(index);
            ref Projectile projectile = ref throwReadyComponent.ThrowableProjectile.Get<Projectile>();
            projectile.Rigidbody.velocity += (throwReadyComponent.ThrowDirection * bouncerComponent.ThrowForce);
            projectile.Vew.SetActive(true);
            throwReadyComponent.ThrowableProjectile.Del<Caught>();
            bouncer.Del<ThrowReady>();
            EntityComponentAdder.AddMover(bouncer, bouncerComponent.Vew, _configuration.BouncerMoveSpeed);
        }
    }
}
