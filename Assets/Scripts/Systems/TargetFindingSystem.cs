using Leopotam.Ecs;
using UnityEngine;

public class TargetFindingSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<Generator>.Exclude<Busy> _freeGeneratorFilter;
    private EcsFilter<Monster, Awakened> _awakenedMonsterFilter;

    public void Run()
    {
        foreach (int index in _awakenedMonsterFilter)
        {
            ref EcsEntity monster = ref _awakenedMonsterFilter.GetEntity(index);
            ref Monster monsterComponent = ref monster.Get<Monster>();
            monsterComponent.Target = GetTarget(monster);
            monster.Del<Awakened>();
            monster.Get<Aggressive>();
        }
    }

    private EcsEntity GetTarget(EcsEntity destroyer)
    {
        switch (_freeGeneratorFilter.IsEmpty())
        {
            case true:

                if (_sceneData.PlayerEntity != EcsEntity.Null)
                {
                    return _sceneData.PlayerEntity;
                }
                break;

            case false:

                foreach (int index in _freeGeneratorFilter)
                {
                    ref EcsEntity generator = ref _freeGeneratorFilter.GetEntity(index);
                    generator.Get<Generator>().Destroyer = destroyer;
                    generator.Get<Busy>();
                    return generator;
                }
                break;
        }

        return _sceneData.PlayerEntity;
    }
}
