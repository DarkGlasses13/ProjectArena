using UnityEngine;
using Leopotam.Ecs;

public class PlayerInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private Configuration _configuration;
    private SceneData _sceneData;

    public void Init()
    {
        EcsEntity playerEntity = _world.NewEntity();
        GameObject playerVew = GameObject.Instantiate(_configuration.PlayerPrefab, _sceneData.MovableCenter, Quaternion.identity, _sceneData.Arena);
        _sceneData.Player = playerVew;
        EntityComponentAdder.AddPlayer(playerEntity);
        EntityComponentAdder.AddMover(playerEntity, _sceneData.Player, _configuration.BouncerMoveSpeed);
        EntityComponentAdder.AddBouncer(playerEntity, playerVew, _configuration.StartBouncerThrowForce, _configuration.BouncerLayer);
    }
}
