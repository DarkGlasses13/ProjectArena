using Leopotam.Ecs;
using UnityEngine;

public class MonsterDeathSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<Monster, Vew, Dead> _deadMonsterFilter;

    public void Run()
    {
        foreach (int index in _deadMonsterFilter)
        {
            ref EcsEntity monster = ref _deadMonsterFilter.GetEntity(index);
            ref Monster monsterComponent = ref monster.Get<Monster>();
            ref Vew vewComponent = ref monster.Get<Vew>();

            vewComponent.Object.SetActive(false);
            vewComponent.Object.transform.localPosition = _sceneData.MonsterPool.localPosition;
            monsterComponent.Target = EcsEntity.Null;
            monster.Del<Aggressive>();
            monster.Get<Sleeping>();
        }
    }
}
