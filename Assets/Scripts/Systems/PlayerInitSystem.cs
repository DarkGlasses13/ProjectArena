using UnityEngine;
using Leopotam.Ecs;

public class PlayerInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private Configuration _staticData;
    private SceneData _sceneData;

    public void Init()
    {
        EcsEntity playerEntity = _world.NewEntity();
        _sceneData.Player = GameObject.Instantiate(_staticData.PlayerPrefab, _sceneData.MovableCenter, Quaternion.identity, _sceneData.Arena).transform;
        EntityComponentAdder.AddPlayer(playerEntity);
        EntityComponentAdder.AddMover(playerEntity, _sceneData.Player, _staticData.PlayerMoveSpeed);
        EntityComponentAdder.AddBouncer(playerEntity, _staticData.StartThrowForce, _sceneData.Player.GetComponent<Collider>(), _staticData.BouncerLayer);
    }
}
