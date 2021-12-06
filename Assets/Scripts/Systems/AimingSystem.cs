using Leopotam.Ecs;
using UnityEngine;

public class AimingSystem : IEcsRunSystem
{
    private Configuration _configuration;
    private SceneData _sceneData;
    private InputData _inputData;
    private EcsFilter<Bouncer, ThrowReady> _throwReadyBouncerFilter;

    public void Run()
    {
        if (_sceneData.MovePanel.activeSelf == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _inputData.IsAiming = true;
                EmitRay(out _inputData.AimInfo);
            }

            if (Input.GetMouseButton(0))
            {
                EmitRay(out _inputData.AimInfo);
                Aim();
            }

            if (Input.GetMouseButtonUp(0))
            {
                _inputData.IsAiming = false;
                EmitRay(out _inputData.AimInfo);
                DeterminThrowDirection();
            }
        }
    }

    private void EmitRay(out RaycastHit hitInfo)
    {
        Physics.Raycast(_sceneData.Camera.ScreenPointToRay(Input.mousePosition), out hitInfo);
    }

    private void Aim()
    {
        Vector3 target = new Vector3(_inputData.AimInfo.point.x, 0, _inputData.AimInfo.point.z);

        foreach (int index in _throwReadyBouncerFilter)
        {
            ref Bouncer bouncerComponent = ref _throwReadyBouncerFilter.Get1(index);
            bouncerComponent.Vew.transform.LookAt(target);
        }
    }

    private void DeterminThrowDirection()
    {
        Vector3 target = new Vector3(_inputData.AimInfo.point.x, 0, _inputData.AimInfo.point.z);

        foreach (int index in _throwReadyBouncerFilter)
        {
            ref EcsEntity bouncer = ref _throwReadyBouncerFilter.GetEntity(index);
            ref Bouncer bouncerComponent = ref bouncer.Get<Bouncer>();
            Vector3 direction = (target - bouncerComponent.Vew.transform.position).normalized;
            bouncer.Get<ThrowReady>().ThrowDirection = direction;
            bouncer.Get<ThrowTrigger>();
        }
    }
}
