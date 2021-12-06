using Leopotam.Ecs;
using UnityEngine;

public class GeneratorInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private Configuration _configuration;
    private SceneData _sceneData;
    private int _generatorsCount = 5;
    private Vector3 _spawnPosition = new Vector3(-4, 0, -9);

    public void Init()
    {
        for (int i = 0; i < _generatorsCount; i++)
        {
            EcsEntity generator = _world.NewEntity();
            GameObject vew = GameObject.Instantiate(_configuration.GeneratorPrefab, _sceneData.Arena);
            EntityComponentAdder.AddGenerator(generator, vew, _configuration.GeneratorLayer);
            vew.transform.localPosition = _spawnPosition;
            _spawnPosition.x += 2;
        }
    }
}
