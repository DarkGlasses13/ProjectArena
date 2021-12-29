using Leopotam.Ecs;
using UnityEngine;

public class RobotDeathSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<Enemy, Vew, Dead> _deadRobotFilter;

    public void Run()
    {
        foreach (int index in _deadRobotFilter)
        {
            ref EcsEntity robot = ref _deadRobotFilter.GetEntity(index);
            ref Enemy robotComponent = ref robot.Get<Enemy>();
            ref Vew vewComponent = ref robot.Get<Vew>();

            vewComponent.Object.SetActive(false);
            vewComponent.Object.transform.localPosition = _sceneData.EnemyPool.localPosition;

            if (robotComponent.Target != _sceneData.PlayerEntity)
            {
                robotComponent.Target.Del<Busy>();

            }

            robotComponent.Target = EcsEntity.Null;
            robot.Del<Aggressive>();
            robot.Get<Sleeping>();
        }
    }
}
