using UnityEngine;
using Leopotam.Ecs;

sealed class TimeControlSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;
    private RuntimeData _runtimeData;
    private EcsFilter<Mover> _moverFilter;
    private EcsFilter<Projectile> _projectileFilter;

    public void Init()
    {
        EcsEntity timeController = _world.NewEntity();
        timeController.Get<TimeController>();
    }

    public void Run()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SlowDown();
        }
    }

    private void SlowDown()
    {
        Time.timeScale = Mathf.Lerp(Time.timeScale, 0.5f, _runtimeData.LerpTime);
    }

    private void SpeedUp(float factor)
    {
        Time.timeScale = Mathf.Lerp(Time.timeScale, Time.timeScale * factor, _runtimeData.LerpTime);
    }
}