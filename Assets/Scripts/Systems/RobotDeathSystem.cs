using Leopotam.Ecs;
using UnityEngine;

public class RobotDeathSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<Robot, Vew, Dead> _deadRobotFilter;

    public void Run()
    {
        foreach (int index in _deadRobotFilter)
        {
            ref EcsEntity robot = ref _deadRobotFilter.GetEntity(index);
            ref Robot robotComponent = ref robot.Get<Robot>();
            ref Vew vewComponent = ref robot.Get<Vew>();

            vewComponent.Object.SetActive(false);
            vewComponent.Object.transform.localPosition = _sceneData.RobotPool.localPosition;

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
