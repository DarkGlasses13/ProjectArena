using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using UnityEngine;

namespace HellBounce 
{
    public sealed class EcsStartup : MonoBehaviour 
    {
        [SerializeField] private ConfigData _configData;
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
                .Add(new PlayerInitSystem())
                .Add(new ProjectileInitSystem())
                .Add(new ServerInitSystem())
                .Add(new EnemyInitSystem())
                .Add(new EnemyFactorySystem())
                .Add(new InputSystem(), SystemName.Input)
                .Add(new TimeControlSystem(), SystemName.Time)
                .Add(new ProjectileAccelerationSystem(), SystemName.Acceleration)
                .Add(new RicochetTrajectoryRenderingSystem(), SystemName.Trajectory)
                .Add(new ProjectileTrackingSystem(), SystemName.Track)
                .Add(new CatchingSystem(), SystemName.Catch)
                .Add(new AimingSystem(), SystemName.Aim)
                .Add(new ThrowingSystem(), SystemName.Throw)
                .Add(new HitSystem(), SystemName.Hit)
                .Add(new AggroSystem(), SystemName.Aggro)
                .Add(new DownloadingSystem(), SystemName.Download)
                .Add(new EnemyAttackSystem(), SystemName.Attack)
                .Add(new EnemyDeathSystem(), SystemName.MonsterDeath)
                .OneFrame<HitTrigger>()
                .OneFrame<ThrowTrigger>()
                .OneFrame<DeadTrigger>()
                .Inject(_updateSystems)
                .Inject(_configData)
                .Inject(_sceneData)
                .Inject(runtimeData)
                .Inject(inputData)
                .InjectUi(_uiEmitter)
                .Init();

            _fixedUpdateSystems
                .Add(new MoveControlSystem(), SystemName.Move)
                .Add(new RicochetSystem(), SystemName.Ricochet)
                .Inject(_updateSystems)
                .Inject(_configData)
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

        public void SetUpdateSystemState(string name, bool state)
        {
            SetSystemState(_updateSystems, name, state);
        }

        public void SetFixedUpdateSystemState(string name, bool state)
        {
            SetSystemState(_fixedUpdateSystems, name, state);
        }

        private void SetSystemState(EcsSystems systems, string name, bool state)
        {
            systems.SetRunSystemState(systems.GetNamedRunSystem(name), state);
        }
    }
}