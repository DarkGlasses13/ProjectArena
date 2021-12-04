using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using UnityEngine;

namespace HellBounce 
{
    sealed class EcsStartup : MonoBehaviour 
    {
        [SerializeField] private Configuration _configuration;
        [SerializeField] private SceneData _sceneData;
        [SerializeField] private EcsUiEmitter _uiEmitter;

        private EcsWorld _world;
        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;

        private void Start() 
        {
            _world = new EcsWorld();
            _updateSystems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);
            RuntimeData runtimeData = new RuntimeData();
            InputData inputData = new InputData();

#if UNITY_EDITOR

            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_updateSystems).name = "[UPDATE SYSTEMS]";
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_fixedUpdateSystems).name = "[FIXEDUPDATE SYSTEMS]";
#endif

            _updateSystems
                .Add(new JoysticInputSystem(), SystemsNames.Joystick)
                .Add(new CatchingSystem(), SystemsNames.Catch)
                .Add(new AimingSystem(), SystemsNames.Aim)
                .Add(new ThrowingSystem(), SystemsNames.Throw)
                .Add(new HideJoysticSystem())
                .Add(new PlayerInitSystem())
                .Add(new ProjectileInitSystem())
                .OneFrame<CatchTrigger>()
                .OneFrame<ThrowTrigger>()
                .Inject(_updateSystems)
                .Inject(_configuration)
                .Inject(_sceneData)
                .Inject(runtimeData)
                .Inject(inputData)
                .InjectUi(_uiEmitter)
                .Init();

            _fixedUpdateSystems
                .Add(new MoveingSystem(), SystemsNames.Move)
                .Inject(_fixedUpdateSystems)
                .Inject(_configuration)
                .Inject(_sceneData)
                .Inject(runtimeData)
                .Inject(inputData)
                .Init();
        }

        private void Update() 
        {
            _updateSystems?.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        private void OnDestroy() 
        {
            _updateSystems?.Destroy();
            _updateSystems = null;
            _world?.Destroy();
            _world = null;
        }
    }
}