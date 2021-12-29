using UnityEngine;
using Leopotam.Ecs;

class RicochetTrajectoryRenderingSystem : IEcsRunSystem
{
    private ConfigData _configData;
    private EcsFilter<Bouncer, Vew, Aimer> _aimingBouncerFilter;

    public void Run()
    {
        foreach (int index in _aimingBouncerFilter)
        {
            ref Bouncer bouncerComponent = ref _aimingBouncerFilter.Get1(index);
            ref Vew vewComponent = ref _aimingBouncerFilter.Get2(index);

            Vector3 offset = new Vector3
            (
                vewComponent.Object.transform.position.x,
                0.6f,
                vewComponent.Object.transform.position.z
            );

            Ray ray = new Ray(offset, vewComponent.Object.transform.forward);
            bouncerComponent.LineRenderer.positionCount = 1;
            bouncerComponent.LineRenderer.SetPosition(0, offset);
            float remainingLength = _configData.TrajectoryLength;

            if (Input.GetMouseButton(0)) { bouncerComponent.LineRenderer.enabled = true; }

            if (Input.GetMouseButton(0) == false) { bouncerComponent.LineRenderer.enabled = false; }

            for (int i = 0; i < _configData.ReflectionsCount; i++)
            {
                if (Physics.Raycast(ray, out RaycastHit hitInfo, remainingLength))
                {
                    bouncerComponent.LineRenderer.positionCount += 1;
                    bouncerComponent.LineRenderer.SetPosition(bouncerComponent.LineRenderer.positionCount - 1, hitInfo.point);
                    remainingLength -= Vector3.Distance(ray.origin, hitInfo.point);
                    ray = new Ray(hitInfo.point, Vector3.Reflect(ray.direction, hitInfo.normal));
                }
                else
                {
                    bouncerComponent.LineRenderer.positionCount += 1;

                    bouncerComponent.LineRenderer.SetPosition
                    (
                        bouncerComponent.LineRenderer.positionCount - 1,
                        ray.origin + ray.direction * remainingLength
                    );
                }
            }
        }
    }
}