using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

public class EnemyFactorySystem : IEcsInitSystem
{
    private ConfigData _configData;
    private SceneData _sceneData;
    private EcsFilter<Enemy, Vew, Sleeping> _sleepingEnemyFilter;

    private float Multiplier
    {
        get
        {
            switch (_configData.EnemySpawnMultiplier)
            {
                case SpawnMultiplier.X1: return 1;

                case SpawnMultiplier.X2: return 2;

                case SpawnMultiplier.X3:return 3;
            }

            return 1;
        }
    }
    public void Init()
    {
        Coroutines.StartRoutine(WakeUp());
    }

    private IEnumerator WakeUp()
    {
        switch (_sleepingEnemyFilter.IsEmpty())
        {
            case true:
                Debug.Log("Robots are disabled.");
                break;

            case false:

                while (true)
                {
                    EcsEntity entity = _sleepingEnemyFilter.GetEntity(Random.Range(0, _sleepingEnemyFilter.GetEntitiesCount()));
                    Vew vewComponent = entity.Get<Vew>();
                    entity.Del<Sleeping>();
                    vewComponent.Object.transform.localPosition = _sceneData.Gates[Random.Range(0, _sceneData.Gates.Length)].localPosition;
                    vewComponent.Object.transform.localRotation = Quaternion.identity;
                    vewComponent.Object.SetActive(true);
                    entity.Get<Awakened>();
                    yield return new WaitForSeconds(Random.Range(0, _configData.MaxTimeBetweenEnemySpawn / Multiplier));
                }
        }
    }
}
