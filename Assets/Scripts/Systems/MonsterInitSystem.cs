using UnityEngine;
using UnityEngine.AI;
using Leopotam.Ecs;

public class MonsterInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private ConfigData _configData;
    private SceneData _sceneData;

    private int _multiplier
    {
        get
        {
            switch (_configData.MonsterSpawnMultiplier)
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
        for (int m = 0; m < _configData.StartMonsterPoolSize * _multiplier; m++)
        {
            GameObject vew = Object.Instantiate(_configData.DefaultMonsterPrefab, _sceneData.Arena);
            EcsEntity entity = _world.NewEntity();
            ref Vew vewComponent = ref entity.Get<Vew>();
            ref Monster monsterComponent = ref entity.Get<Monster>();
            vewComponent.Object = vew;
            vewComponent.Object.layer = _configData.MonsterLayer;
            vewComponent.Object.SetActive(false);
            vewComponent.Object.transform.localPosition = _sceneData.MonsterPool.localPosition;
            monsterComponent.MoveSpeed = _configData.DefaultMonsterMoveSpeed;
            monsterComponent.NavMeshAgent = vewComponent.Object.GetComponent<NavMeshAgent>();
            Transmitter transmitter = vewComponent.Object.AddComponent<Transmitter>();
            transmitter.Type = TransmitterType.Monster;
            transmitter.Entity = entity;
            entity.Get<Sleeping>();
        }
    }
}
