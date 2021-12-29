using UnityEngine;
using Leopotam.Ecs;

public class PlayerInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private ConfigData _configData;
    private SceneData _sceneData;
    

    public void Init()
    {
        GameObject vewObject = Object.Instantiate
        (
            _configData.PlayerPrefab,
            _sceneData.PlayerSpawnPoint.localPosition,
            Quaternion.identity,
            _sceneData.Arena
        );

        EcsEntity entity = _world.NewEntity();
        entity.Get<Player>();
        ref Vew vewComponent = ref entity.Get<Vew>();
        ref Mover moverComponent = ref entity.Get<Mover>();
        ref Bouncer bouncerComponent = ref entity.Get<Bouncer>();

        vewComponent.Object = vewObject;
        _sceneData.PlayerEntity = entity;
        moverComponent.Controler = vewComponent.Object.GetComponent<CharacterController>();
        moverComponent.MoveSpeed = _configData.BouncerMoveSpeed;
        moverComponent.rotationSmooth = _configData.RotationSmooth;
        bouncerComponent.ThrowForce = _configData.ThrowForce;
        bouncerComponent.LineRenderer = vewComponent.Object.GetComponent<LineRenderer>();
        Transmitter transmitter = vewComponent.Object.AddComponent<Transmitter>();
        transmitter.Type = TransmitterType.Bouncer;
        transmitter.Entity = entity;
    }
}
