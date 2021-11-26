using Leopotam.Ecs;
using UnityEngine;

namespace HellBounce 
{
    sealed class EcsStartup : MonoBehaviour 
    {
        public StaticData StaticData;
        public SceneData SceneData;

        private EcsWorld _world;
        private EcsSystems _systems;

        void Start() 
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            RuntimeData runtimeData = new RuntimeData();
            InputData inputData = new InputData();

#if UNITY_EDITOR

            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif

            _systems
                .Add(new InputSystem())
                .Add(new BouncerInitSystem())
                .Add(new MoveControlSystem())
                .Inject(StaticData)
                .Inject(SceneData)
                .Inject(runtimeData)
                .Inject(inputData)
                .Init();
        }

        void Update() 
        {
            _systems?.Run();
        }

        void OnDestroy() 
        {
            _systems?.Destroy();
            _systems = null;
            _world?.Destroy();
            _world = null;
        }
    }
}