using Leopotam.Ecs;
using UnityEngine;

public class AimingSystem : IEcsRunSystem
{
    private ConfigData _configData;
    private SceneData _sceneData;
    private InputData _inputData;
    private EcsFilter<Catcher>.Exclude<Aimer> _catcherFilter;
    private EcsFilter<Aimer> _aimerFilter;

    public void Run()
    {
        foreach (int index in _catcherFilter)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ref EcsEntity bouncer = ref _catcherFilter.GetEntity(index);
                bouncer.Get<Aimer>();
                _sceneData.VirtualJoystic.Disable();
            }
        }

        foreach (int index in _aimerFilter)
        {
            ref EcsEntity bouncer = ref _aimerFilter.GetEntity(index);
            ref Bouncer bouncerComponent = ref bouncer.Get<Bouncer>();
            ref Vew vewComponent = ref bouncer.Get<Vew>();

            Vector3 aimingPoint = new Vector3(_inputData.ClickInfo.point.x, 0, _inputData.ClickInfo.point.z);
            Vector3 directionToAimingPoint = aimingPoint - vewComponent.Object.transform.position;
            Quaternion aimingRotation = Quaternion.LookRotation(directionToAimingPoint);

            if (Input.GetMouseButton(0))
            {
                EmitRay(out _inputData.ClickInfo);

                vewComponent.Object.transform.rotation = Quaternion.Lerp
                    (
                        vewComponent.Object.transform.rotation,
                        aimingRotation,
                        _configData.BouncerRotationSmooth * Time.deltaTime
                    );
            }

            if (Input.GetMouseButtonUp(0))
            {
                EmitRay(out _inputData.ClickInfo);
                Vector3 direction = (aimingPoint - vewComponent.Object.transform.position).normalized;
                bouncer.Get<Aimer>().ThrowDirection = direction;
                bouncer.Get<ThrowTrigger>();
            }
        }
    }

    private void EmitRay(out RaycastHit hitInfo)
    {
        Physics.Raycast(_sceneData.Camera.ScreenPointToRay(Input.mousePosition), out hitInfo);
    }
}
