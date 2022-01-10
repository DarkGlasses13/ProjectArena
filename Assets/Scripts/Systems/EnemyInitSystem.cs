using UnityEngine;
using UnityEngine.AI;
using Leopotam.Ecs;

public class EnemyInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private ConfigData _configData;
    private SceneData _sceneData;
    private const float _angularSpeed = 500;
    private const float _acceleration = 100;

    private int _multiplier
    {
        get
        {
            switch (_configData.EnemySpawnMultiplier)
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
        for (int m = 0; m < _configData.EnemyPoolSize * _multiplier; m++)
        {
            GameObject vew = Object.Instantiate(_configData.DefaultEnemyPrefab, _sceneData.EnemyPool);
            EcsEntity entity = _world.NewEntity();
            ref Vew vewComponent = ref entity.Get<Vew>();
            ref Enemy EnemyComponent = ref entity.Get<Enemy>();
            vewComponent.Object = vew;
            vewComponent.Animator = vewComponent.Object.GetComponent<Animator>();
            vewComponent.Object.layer = _configData.EnemyLayer;
            vewComponent.Object.SetActive(false);
            vewComponent.Object.transform.localPosition = _sceneData.EnemyPool.localPosition;
            EnemyComponent.MoveSpeed = _configData.DefaultEnemyMoveSpeed;
            EnemyComponent.NavMeshAgent = vewComponent.Object.GetComponent<NavMeshAgent>();
            Transmitter transmitter = vewComponent.Object.AddComponent<Transmitter>();
            transmitter.Type = TransmitterType.Enemy;
            transmitter.Entity = entity;
            EnemyComponent.NavMeshAgent.speed = EnemyComponent.MoveSpeed;
            EnemyComponent.NavMeshAgent.angularSpeed = _angularSpeed;
            EnemyComponent.NavMeshAgent.acceleration = _acceleration;
            entity.Get<Sleeping>();
        }
    }
}
