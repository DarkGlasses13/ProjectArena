using Leopotam.Ecs;
using System;
using UnityEngine;

public class TargetFindingSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<Generator>.Exclude<Busy> _freeGeneratorFilter;
    private EcsFilter<Enemy, Awakened> _awakenedRobotFilter;

    public void Run()
    {
        foreach (int index in _awakenedRobotFilter)
        {
            ref EcsEntity robot = ref _awakenedRobotFilter.GetEntity(index);
            ref Enemy robotComponent = ref robot.Get<Enemy>();
            robotComponent.Target = GetTarget(robot, out float stoppingDistance);
            robotComponent.NavMeshAgent.stoppingDistance = stoppingDistance;
            robot.Del<Awakened>();
            robot.Get<Aggressive>();
        }
    }

    private EcsEntity GetTarget(EcsEntity destroyer, out float stoppingDistance)
    {
        if (_freeGeneratorFilter.IsEmpty() == false)
        {
            foreach (int index in _freeGeneratorFilter)
            {
                ref EcsEntity generator = ref _freeGeneratorFilter.GetEntity(index);
                ref Generator generatorComponent = ref generator.Get<Generator>();
                generatorComponent.Destroyer = destroyer;
                stoppingDistance = 2f;
                generator.Get<Busy>();
                return generator;
            }
        }

        stoppingDistance = 2f;
        return _sceneData.PlayerEntity;
    }
}
