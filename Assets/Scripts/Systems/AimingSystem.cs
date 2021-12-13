using Leopotam.Ecs;
using UnityEngine;

public class AimingSystem : IEcsRunSystem
{
    private ConfigData _configData;
    private SceneData _sceneData;
    private InputData _inputData;
    private EcsFilter<ThrowReady>.Exclude<Aiming> _throwReadyFilter;
    private EcsFilter<ThrowReady, Aiming> _aimingFilter;

    public void Run()
    {
        foreach (int index in _throwReadyFilter)
        {
            if (_sceneData.Joystick.IsHiden)
            {
                _sceneData.Joystick.enabled = false;

                if (Input.GetMouseButtonDown(0))
                {
                    _throwReadyFilter.GetEntity(index).Get<Aiming>();
                }
            }
        }

        foreach (int index in _aimingFilter)
        {
            ref EcsEntity bouncer = ref _aimingFilter.GetEntity(index);
            ref Bouncer bouncerComponent = ref bouncer.Get<Bouncer>();
            ref Vew vewComponent = ref bouncer.Get<Vew>();

            Vector3 aimingPoint = new Vector3(_inputData.TouchInfo.point.x, 0, _inputData.TouchInfo.point.z);
            Vector3 directionToAimingPoint = aimingPoint - vewComponent.Object.transform.position;
            Quaternion aimingRotation = Quaternion.LookRotation(directionToAimingPoint);

            if (Input.GetMouseButton(0))
            {
                EmitRay(out _inputData.TouchInfo);

                vewComponent.Object.transform.rotation = Quaternion.Lerp
                    (
                        vewComponent.Object.transform.rotation,
                        aimingRotation,
                        _configData.RotationSmooth * Time.deltaTime
                    );
            }

            if (Input.GetMouseButtonUp(0))
            {
                EmitRay(out _inputData.TouchInfo);
                Vector3 direction = (aimingPoint - vewComponent.Object.transform.position).normalized;
                bouncer.Get<ThrowReady>().ThrowDirection = direction;
                bouncer.Get<ThrowTrigger>();
            }
        }
    }

    private void EmitRay(out RaycastHit hitInfo)
    {
        Physics.Raycast(_sceneData.Camera.ScreenPointToRay(Input.mousePosition), out hitInfo);
    }
}
