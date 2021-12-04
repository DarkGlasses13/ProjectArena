using Leopotam.Ecs;
using UnityEngine;

public static class EntityComponentAdder
{
    private const int _moveSpeedPercentModifire = 200;
    private const int _throwForcePercentModifire = 3;

    public static Player AddPlayer(EcsEntity entity)
    {
        ref Player playerComponent = ref entity.Get<Player>();
        return playerComponent;
    }

    public static Mover AddMover(EcsEntity entity, GameObject vew, float moveSpeed)
    {
        ref Mover mover = ref entity.Get<Mover>();
        mover.Vew = vew;
        mover.MoveSpeed = moveSpeed / _moveSpeedPercentModifire;
        return mover;
    }

    public static Bouncer AddBouncer(EcsEntity entity, GameObject vew, float throwForce, int bouncerLayer)
    {
        ref Bouncer bouncerComponent = ref entity.Get<Bouncer>();
        bouncerComponent.ThrowForce = throwForce / _throwForcePercentModifire;
        bouncerComponent.Vew = vew;
        CatchTransmitter catchHandler = bouncerComponent.Vew.AddComponent<CatchTransmitter>();
        catchHandler.Entity = entity;
        bouncerComponent.Vew.layer = bouncerLayer;
        return bouncerComponent;
    }

    public static Projectile AddProjectile(EcsEntity entity, GameObject vew, int projectileLayer)
    {
        ref Projectile projectileComponent = ref entity.Get<Projectile>();
        projectileComponent.Vew = vew;
        projectileComponent.Rigidbody = projectileComponent.Vew.GetComponent<Rigidbody>();
        ShotTransmitter shotTransmitter = vew.AddComponent<ShotTransmitter>();
        shotTransmitter.Entity = entity;
        projectileComponent.Vew.layer = projectileLayer;
        return projectileComponent;
    }
}