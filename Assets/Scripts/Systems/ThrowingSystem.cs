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
            ref Catcher catcher = ref bouncer.Get<Catcher>();
            ref Mover mover = ref bouncer.Get<Mover>();

            ref EcsEntity projectile = ref catcher.ThrowableProjectile;
            ref Projectile projectileComponent = ref projectile.Get<Projectile>();
            ref Vew projectileVew = ref projectile.Get<Vew>();

            projectileVew.Object.transform.SetParent(_sceneData.Arena);
            projectileVew.Object.SetActive(true);
            catcher.ThrowableProjectile.Del<Caught>();
            bouncer.Del<Aimer>();
            bouncer.Del<Catcher>();
            _sceneData.VirtualJoystic.Enable();
            mover.Controler = bouncer.Get<Vew>().Object.GetComponent<CharacterController>();
            mover.MoveSpeed = _configData.BouncerMoveSpeed;
            mover.rotationSmooth = _configData.BouncerRotationSmooth;
        }
    }
}
