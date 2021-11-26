using UnityEngine;
using Leopotam.Ecs;

public class BouncerInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private StaticData _staticData;
    private SceneData _sceneData;

    public void Init()
    {
        EcsEntity bouncerEntity = _world.NewEntity();
        bouncerEntity.Get<Bouncer>();
        ref Mover moveController = ref bouncerEntity.Get<Mover>();
        moveController.MoveSpeed = _staticData.BouncerMoveSpeed;
        GameObject bouncer = GameObject.Instantiate(_staticData.BouncerPrefab, _sceneData.MovableCenter, Quaternion.identity, _sceneData.Arena);
        moveController.MovableObject = bouncer.transform;
        _sceneData.Bouncer = bouncer.transform;
    }
}
