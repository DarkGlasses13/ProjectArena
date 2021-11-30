using Leopotam.Ecs;
using UnityEngine;

public class ProjectileInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private Configuration _staticData;
    private SceneData _sceneData;

    public void Init()
    {
        EcsEntity projectileEntity = _world.NewEntity();
        GameObject projectile = GameObject.Instantiate(_staticData.ProjectilePrefab, _sceneData.Player.position, Quaternion.identity, _sceneData.Arena);
        Projectile projectileComponent = EntityComponentAdder.AddProjectile(projectileEntity, projectile, _staticData.ProjectileLayer);
    }
}
