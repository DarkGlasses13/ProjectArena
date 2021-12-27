using Leopotam.Ecs;
using UnityEngine;

public class CatchingSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<Bouncer, Vew>.Exclude<ThrowReady, Aiming> _bouncerFilter;
    private EcsFilter<CatchTrigger> _catchTriggerFilter;
    
    private Vector3 _catchPosition = new Vector3(0, 0, 1f);

    public void Run()
    {
        foreach (int index in _bouncerFilter)
        {
            ref EcsEntity bouncer = ref _bouncerFilter.GetEntity(index);
            ref Bouncer bouncerComponent = ref bouncer.Get<Bouncer>();
            ref Vew vewComponent = ref bouncer.Get<Vew>();
            Vector3 rayOffset = new Vector3(vewComponent.Object.transform.position.x, 0.6f, vewComponent.Object.transform.position.z);
            Ray ray = new Ray(rayOffset, vewComponent.Object.transform.forward);
            float rayLength = 0.5f;

            if
            (
                Physics.Raycast(ray, out RaycastHit catchInfo, rayLength) &&
                catchInfo.collider.TryGetComponent(out Transmitter transmitter) &&
                transmitter.Type == TransmitterType.Projectile
            ) 
            {
                ref Caught caughtComponent = ref transmitter.Entity.Get<Caught>();
                caughtComponent.Catcher = bouncer;
                transmitter.Entity.Get<CatchTrigger>();
            }
        }

        foreach (int index in _catchTriggerFilter)
        {
            ref EcsEntity projectile = ref _catchTriggerFilter.GetEntity(index);
            ref Projectile projectileComponent = ref projectile.Get<Projectile>();
            ref Vew projectileVewComponent = ref projectile.Get<Vew>();
            ref Caught caughtComponent = ref projectile.Get<Caught>();

            ref EcsEntity bouncer = ref caughtComponent.Catcher;
            ref Vew boucerVewComponent = ref bouncer.Get<Vew>();
            ref ThrowReady throwReadyComponent = ref bouncer.Get<ThrowReady>();
            
            projectileVewComponent.Object.transform.SetParent(boucerVewComponent.Object.transform);
            projectileVewComponent.Object.transform.localPosition = _catchPosition;
            projectileVewComponent.Object.transform.localRotation = new Quaternion(0, 0, 0, 0);
            projectileVewComponent.Object.SetActive(false);

            throwReadyComponent.ThrowableProjectile = projectile;
            _sceneData.Startup.SetFixedUpdateSystemState(SystemName.Move, false);
            _sceneData.Startup.SetUpdateSystemState(SystemName.Aim, true);
        }
    }
}
