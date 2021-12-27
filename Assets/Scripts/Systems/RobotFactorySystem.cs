using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

public class RobotFactorySystem : IEcsInitSystem
{
    private ConfigData _configData;
    private SceneData _sceneData;
    private EcsFilter<Robot, Vew, Sleeping> _sleepingRobotFilter;

    private float Multiplier
    {
        get
        {
            switch (_configData.RobotSpawnMultiplier)
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
        switch (_sleepingRobotFilter.IsEmpty())
        {
            case true:
                Debug.Log("Robots are disabled.");
                break;

            case false:

                while (true)
                {
                    EcsEntity robot = _sleepingRobotFilter.GetEntity(Random.Range(0, _sleepingRobotFilter.GetEntitiesCount()));
                    Vew vewComponent = robot.Get<Vew>();
                    robot.Del<Sleeping>();
                    vewComponent.Object.transform.localPosition = _sceneData.Gates[Random.Range(0, _sceneData.Gates.Length)].localPosition;
                    vewComponent.Object.transform.localRotation = Quaternion.identity;
                    vewComponent.Object.SetActive(true);
                    robot.Get<Awakened>();
                    yield return new WaitForSeconds(Random.Range(0, _configData.MaxTimeBetweenRobotSpawn / Multiplier));
                }
        }
    }
}
