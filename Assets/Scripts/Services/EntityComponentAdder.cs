using Leopotam.Ecs;
using UnityEngine;

public static class EntityComponentAdder
{
    public static Player AddPlayer(EcsEntity entity)
    {
        ref Player playerComponent = ref entity.Get<Player>();
        return playerComponent;
    }

    public static Mover AddMover(EcsEntity entity, Transform vew, float moveSpeed)
    {
        ref Mover mover = ref entity.Get<Mover>();
        mover.Vew = vew;
        mover.MoveSpeed = moveSpeed;
        return mover;
    }

    public static Bouncer AddBouncer(EcsEntity entity, float throwForce, Collider collider, int bouncerLayer)
    {
        ref Bouncer bouncerComponent = ref entity.Get<Bouncer>();
        bouncerComponent.ThrowForce = throwForce;
        bouncerComponent.Collider = collider;
        CatchHandler catchHandler = bouncerComponent.Collider.gameObject.AddComponent<CatchHandler>();
        catchHandler.Entity = entity;
        bouncerComponent.Collider.gameObject.layer = bouncerLayer;
        return bouncerComponent;
    }

    public static Clickable AddClickable(EcsEntity entity, GameObject vew, int clickableLayer)
    {
        ref Clickable clickableComponent = ref entity.Get<Clickable>();
        clickableComponent.Vew = vew;
        clickableComponent.Vew.layer = clickableLayer;
        ClickHandler clickHandler = clickableComponent.Vew.AddComponent<ClickHandler>();
        clickHandler.Entity = entity;
        return clickableComponent;
    }

    public static Button AddButton(EcsEntity entity, GameObject vew, int clickableLayer)
    {
        ref Button buttonComponent = ref entity.Get<Button>();
        AddClickable(entity, vew, clickableLayer);
        return buttonComponent;
    }

    public static MoveButton AddMoveButton(EcsEntity entity, GameObject vew, int clickableLayer, MoveSide moveSide)
    {
        AddButton(entity, vew, clickableLayer);
        ref MoveButton moveButtonComponent = ref entity.Get<MoveButton>();
        moveButtonComponent.MoveSide = moveSide;
        return moveButtonComponent;
    }

    public static Projectile AddProjectile(EcsEntity entity, GameObject vew, int projectileLayer)
    {
        ref Projectile projectileComponent = ref entity.Get<Projectile>();
        projectileComponent.Rigidbody = vew.GetComponent<Rigidbody>();
        ShotHandler shotHandler = vew.AddComponent<ShotHandler>();
        shotHandler.Projectile = projectileComponent;
        projectileComponent.Rigidbody.gameObject.layer = projectileLayer;
        return projectileComponent;
    }
}
