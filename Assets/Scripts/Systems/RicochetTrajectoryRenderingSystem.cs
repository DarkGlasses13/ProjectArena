using UnityEngine;
using Leopotam.Ecs;
using System.Collections.Generic;

class RicochetTrajectoryRenderingSystem : IEcsInitSystem, IEcsRunSystem
{
    private ConfigData _configData;
    private EcsFilter<Bouncer, Vew> _bouncerFilter;
    private EcsFilter<Bouncer, Vew, Aimer> _aimingBouncerFilter;
    private List<LineRenderer> _trajectoryLines = new List<LineRenderer>();

    public void Init()
    {
        foreach (int index in _bouncerFilter)
        {
            ref Bouncer bouncerComponent = ref _bouncerFilter.Get1(index);
            ref Vew bouncerVewComponent = ref _bouncerFilter.Get2(index);
            int lastReflectionIndex = _configData.StartReflectionsCount - 1;

            for (int i = 0; i < _configData.StartReflectionsCount; i++)
            {
                if (i != lastReflectionIndex)
                {
                    InitLines(_configData.TrajectoryLine, bouncerVewComponent.Object.transform);
                }
                else
                {
                    InitLines(_configData.EndTrajectoryLine, bouncerVewComponent.Object.transform);
                }
            }
        }
    }

    public void Run()
    {
        foreach (int index in _aimingBouncerFilter)
        {
            ref Bouncer bouncerComponent = ref _aimingBouncerFilter.Get1(index);
            ref Vew vewComponent = ref _aimingBouncerFilter.Get2(index);

            Vector3 offset = new Vector3
            (
                vewComponent.Object.transform.position.x,
                1f,
                vewComponent.Object.transform.position.z
            );

            Ray ray = new Ray(offset, vewComponent.Object.transform.forward);

            for (int i = 0; i < _configData.StartReflectionsCount; i++)
            {
                switch (Physics.Raycast(ray, out RaycastHit hitInfo, _configData.StartTrajectoryLength))
                {
                    case true:
                        DrawLine(_trajectoryLines[i], ray.origin, hitInfo.point);
                        ray = new Ray(hitInfo.point, Vector3.Reflect(ray.direction, hitInfo.normal));
                        break;

                    case false:
                        EraseLine(_trajectoryLines[i]);
                        break;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                EraseAllLines();
            }
        }
    }

    private void InitLines(GameObject line, Transform bouncer)
    {
        GameObject instantiatedLine = Object.Instantiate(line, bouncer);
        instantiatedLine.SetActive(false);
        LineRenderer lineRenderer = instantiatedLine.GetComponent<LineRenderer>();
        _trajectoryLines.Add(lineRenderer);
    }

    private void DrawLine(LineRenderer lineRenderer, Vector3 startPosition, Vector3 endPosition)
    {
        lineRenderer.gameObject.SetActive(true);
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }

    private void EraseLine(LineRenderer lineRenderer)
    {
        lineRenderer.gameObject.SetActive(false);
    }

    private void EraseAllLines()
    {
        foreach (LineRenderer lineRenderer in _trajectoryLines)
        {
            lineRenderer.gameObject.SetActive(false);
        }
    }
}