using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

public class MonsterFactorySystem : IEcsInitSystem
{
    private ConfigData _configData;
    private SceneData _sceneData;
    private EcsFilter<Monster, Vew, Sleeping> _sleepingMonsterFilter;

    public void Init()
    {
        Coroutines.StartRoutine(WakeUp());
    }

    private IEnumerator WakeUp()
    {
        switch (_sleepingMonsterFilter.IsEmpty())
        {
            case true:
                Debug.Log("Monsters are disabled.");
                break;

            case false:

                while (true)
                {
                    EcsEntity monster = _sleepingMonsterFilter.GetEntity(Random.Range(0, _sleepingMonsterFilter.GetEntitiesCount()));
                    Vew vewComponent = monster.Get<Vew>();
                    monster.Del<Sleeping>();
                    vewComponent.Object.transform.localPosition = _sceneData.Gates[Random.Range(0, _sceneData.Gates.Length)].localPosition;
                    vewComponent.Object.transform.localRotation = Quaternion.identity;
                    vewComponent.Object.SetActive(true);
                    monster.Get<Awakened>();
                    yield return new WaitForSeconds(Random.Range(0, _configData.MaxTimeBetweenMonsterSpawn));
                }
        }
    }
}
