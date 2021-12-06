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
            ref Projectile projectileComponent = ref throwReadyComponent.ThrowableProjectile.Get<Projectile>();
            projectileComponent.Vew.transform.SetParent(_sceneData.Arena);
            projectileComponent.Rigidbody.velocity += (throwReadyComponent.ThrowDirection * bouncerComponent.ThrowForce);
            projectileComponent.Vew.SetActive(true);
            throwReadyComponent.ThrowableProjectile.Del<Caught>();
            bouncer.Del<ThrowReady>();
            EntityComponentAdder.AddMover(bouncer, bouncerComponent.Vew, _configuration.BouncerMoveSpeed, _configuration.RotationSmooth);
        }
    }
}
