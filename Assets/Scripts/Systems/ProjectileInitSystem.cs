using Leopotam.Ecs;
using UnityEngine;

public class ProjectileInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private ConfigData _configData;
    private SceneData _sceneData;

    public void Init()
    {
        GameObject vewObject = Object.Instantiate
        (
            _configData.ProjectilePrefab,
            _sceneData.ProjectileSpawnPoint.localPosition,
            new Quaternion(0, 180, 0, 0),
            _sceneData.Arena
        );

        EcsEntity entity = _world.NewEntity();
        ref Projectile projectileComponent = ref entity.Get<Projectile>();
        ref Vew vewComponent = ref entity.Get<Vew>();

        vewComponent.Object = vewObject;
        Transmitter transmitter = vewComponent.Object.AddComponent<Transmitter>();
        transmitter.Type = TransmitterType.Projectile;
        transmitter.Entity = entity;
    }
}
