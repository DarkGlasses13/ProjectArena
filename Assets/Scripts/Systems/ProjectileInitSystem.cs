using Leopotam.Ecs;
using UnityEngine;

public class ProjectileInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private Configuration _configuration;
    private SceneData _sceneData;
    private Vector3 _shotDirection = new Vector3(0, 0, -10);

    public void Init()
    {
        EcsEntity projectile = _world.NewEntity();
        GameObject vew = GameObject.Instantiate(_configuration.ProjectilePrefab, _sceneData.ProjectileSpawner.position, Quaternion.identity, _sceneData.Arena);
        EntityComponentAdder.AddProjectile(projectile, vew, _configuration.ProjectileLayer);
        projectile.Get<Projectile>().Rigidbody.velocity += _shotDirection;

    }
}
