using Leopotam.Ecs;
using UnityEngine;

public class EnemyDeathSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<Enemy, Vew, DeadTrigger> _deadEnemyFilter;

    public void Run()
    {
        foreach (int index in _deadEnemyFilter)
        {
            ref EcsEntity enemy = ref _deadEnemyFilter.GetEntity(index);
            ref Enemy enemyComponent = ref enemy.Get<Enemy>();
            ref Vew vewComponent = ref enemy.Get<Vew>();

            vewComponent.Object.SetActive(false);
            vewComponent.Object.transform.SetParent(_sceneData.EnemyPool);
            vewComponent.Object.transform.position = _sceneData.EnemyPool.position;

            if (enemyComponent.Target != _sceneData.PlayerEntity)
            {
                enemyComponent.Target.Del<Busy>();

            }

            enemyComponent.Target = EcsEntity.Null;
            enemy.Del<Downloader>();
            enemy.Del<PlayerAttacker>();
            enemy.Del<Awakened>();
            enemy.Get<Sleeping>();
        }
    }
}
