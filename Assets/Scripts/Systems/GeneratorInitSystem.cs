using Leopotam.Ecs;
using UnityEngine;

public class GeneratorInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private ConfigData _configData;
    private SceneData _sceneData;
    private int _generatorsCount = 5;
    private float _distance = 2f;
    private Vector3 _startSpawnPosition = new Vector3(-4f, 0, -10.5f);

    public void Init()
    {
        for (int i = 0; i < _generatorsCount; i++)
        {
            GameObject vew = GameObject.Instantiate(_configData.GeneratorPrefab, _sceneData.Arena);
            EcsEntity entity = _world.NewEntity();
            ref Vew vewComponent = ref entity.Get<Vew>();
            ref Generator generatorComponent = ref entity.Get<Generator>();

            vewComponent.Object = vew;
            vewComponent.Object.layer = _configData.GeneratorLayer;
            vewComponent.Object.transform.localPosition = _startSpawnPosition;
            Transmitter transmitter = vewComponent.Object.AddComponent<Transmitter>();
            transmitter.Type = TransmitterType.Generator;
            transmitter.Entity = entity;
            _startSpawnPosition.x += _distance;
            generatorComponent.Helth = _configData.GeneratorHelth;
        }
    }
}
