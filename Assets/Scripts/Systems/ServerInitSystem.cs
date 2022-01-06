using Leopotam.Ecs;
using UnityEngine;

public class ServerInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private ConfigData _configData;
    private SceneData _sceneData;
    private int _serversCount = 5;
    private float _distance = 2f;
    private Vector3 _startSpawnPosition = new Vector3(-4f, 0, -9f);

    public void Init()
    {
        for (int i = 0; i < _serversCount; i++)
        {
            GameObject vew = GameObject.Instantiate(_configData.GeneratorPrefab, _sceneData.Actors);
            EcsEntity server = _world.NewEntity();
            ref Vew vewComponent = ref server.Get<Vew>();
            ref Server serverComponent = ref server.Get<Server>();

            vewComponent.Object = vew;
            vewComponent.Object.layer = _configData.ServerLayer;
            vewComponent.Object.transform.localPosition = _startSpawnPosition;
            Transmitter transmitter = vewComponent.Object.AddComponent<Transmitter>();
            transmitter.Type = TransmitterType.Server;
            transmitter.Entity = server;
            _startSpawnPosition.x += _distance;
            serverComponent.DataCount = _configData.GeneratorHelth;
        }
    }
}
