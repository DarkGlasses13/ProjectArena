using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

public class MonsterFactorySystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<Monster, Sleeping> _sleepsMonsterFilter;
    private EcsFilter<Monster, Awakened> _awakenedMonsterFilter;
    private EcsFilter<Monster, Aggressive> _aggressiveMonsterFilter;
    private EcsFilter<Monster, Dead> _deadMonsterFilter;

    public void Run()
    {
        
    }

    //private IEnumerator WakeUpAndRelease()
    //{
    //    foreach (int index in _sleepsMonsterFilter)
    //    {
    //        EcsEntity monster = _sleepsMonsterFilter.GetEntity(Random.Range(0, _sleepsMonsterFilter.GetEntitiesCount()));
    //        Monster monsterComponent = monster.Get<Monster>();
    //        monster.Get<Awakened>();
    //        monsterComponent.Vew.SetActive(true);
    //    }
    //}
}
