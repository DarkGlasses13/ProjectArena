using Leopotam.Ecs;
using UnityEngine;

public class MonsterInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private Configuration _configuration;
    private SceneData _sceneData;

    private Vector3 _leftPoolPosition
    {
        get
        {
            return new Vector3(_sceneData.LeftMonsterGates.position.x - 2, _sceneData.LeftMonsterGates.position.y, _sceneData.LeftMonsterGates.position.z);
        }
    }
    private Vector3 _rightPoolPosition
    {
        get
        {
            return new Vector3(_sceneData.RightMonsterGates.position.x + 2, _sceneData.RightMonsterGates.position.y, _sceneData.RightMonsterGates.position.z);
        }
    }
    private int _multiplier
    {
        get
        {
            switch (_configuration.MonsterSpawnMultiplier)
            {
                case SpawnMultiplier.X1:
                    return 1;

                case SpawnMultiplier.X2:
                    return 2;

                case SpawnMultiplier.X3:
                    return 3;
            }

            return 1;
        }
    }


    public void Init()
    {
        CreatePool(_leftPoolPosition);
        CreatePool(_rightPoolPosition);
    }

    private void CreatePool(Vector3 poolPosition)
    {
        for (int m = 0; m < _configuration.StartMonsterPoolSize * _multiplier; m++)
        {
            EcsEntity monster = _world.NewEntity();
            GameObject vew = GameObject.Instantiate(_configuration.DefaultMonsterPrefab, _sceneData.Arena);
            vew.transform.position = poolPosition;
            Monster monsterComponent = EntityComponentAdder.AddMonster(monster, vew);
            monster.Get<Sleeping>();
        }
    }
}
