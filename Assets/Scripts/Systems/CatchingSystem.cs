using Leopotam.Ecs;
using UnityEngine;

public class CatchingSystem : IEcsRunSystem
{
    private ConfigData _configData;
    private SceneData _sceneData;
    private EcsFilter<Bouncer, Vew>.Exclude<Aimer, Catcher> _bouncerFilter;
    
    private Vector3 _catchPosition = new Vector3(0, 0, 1f);

    public void Run()
    {
        foreach (int index in _bouncerFilter)
        {
            ref EcsEntity bouncer = ref _bouncerFilter.GetEntity(index);
            ref Vew bouncerVewComponent = ref _bouncerFilter.Get2(index);

            Vector3 rayOffset = new Vector3(bouncerVewComponent.Object.transform.position.x, 0.6f, bouncerVewComponent.Object.transform.position.z);
            Ray ray = new Ray(rayOffset, bouncerVewComponent.Object.transform.forward);
            float rayLength = 0.7f;

            if
            (
                Physics.Raycast(ray, out RaycastHit catchInfo, rayLength) &&
                catchInfo.collider.TryGetComponent(out Transmitter transmitter) &&
                transmitter.Type == TransmitterType.Projectile
            ) 
            {
                ref Catcher catcherComponent = ref bouncer.Get<Catcher>();
                ref Caught caughtComponent = ref transmitter.Entity.Get<Caught>();
                ref Vew projectileVewComponent = ref transmitter.Entity.Get<Vew>();

                catcherComponent.ThrowableProjectile = transmitter.Entity;
                caughtComponent.Catcher = bouncer;
                projectileVewComponent.Object.transform.SetParent(bouncerVewComponent.Object.transform);
                projectileVewComponent.Object.transform.localPosition = _catchPosition;
                projectileVewComponent.Object.transform.localRotation = new Quaternion(0, 0, 0, 0);
                projectileVewComponent.Object.SetActive(false);
                bouncer.Del<Mover>();
            }
        }
    }
}
