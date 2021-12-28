using Leopotam.Ecs;
using UnityEngine;

public class ThrowingSystem : IEcsRunSystem
{
    private ConfigData _configData;
    private SceneData _sceneData;
    private EcsFilter<ThrowTrigger> _throwFilter;

    public void Run()
    {
        foreach (int index in _throwFilter)
        {
            ref EcsEntity bouncer = ref _throwFilter.GetEntity(index);
            ref Catcher catcherComponent = ref bouncer.Get<Catcher>();
            ref Mover bouncerMoverComponent = ref bouncer.Get<Mover>();

            ref EcsEntity projectile = ref catcherComponent.ThrowableProjectile;
            ref Projectile projectileComponent = ref projectile.Get<Projectile>();
            ref Vew projectileVewComponent = ref projectile.Get<Vew>();

            projectileVewComponent.Object.transform.SetParent(_sceneData.Arena);
            projectileVewComponent.Object.SetActive(true);

            bouncerMoverComponent.Controler = bouncer.Get<Vew>().Object.GetComponent<CharacterController>();
            bouncerMoverComponent.MoveSpeed = _configData.BouncerMoveSpeed;
            bouncerMoverComponent.rotationSmooth = _configData.RotationSmooth;
            catcherComponent.ThrowableProjectile.Del<Caught>();
            bouncer.Del<Aimer>();
            bouncer.Del<Catcher>();
            _sceneData.VirtualJoystic.Enable();
        }
    }
}
