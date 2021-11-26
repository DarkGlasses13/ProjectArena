using Leopotam.Ecs;
using UnityEngine;

public class InputSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private InputData _inputData;

    public void Run()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EmitRay(out _inputData.TouchInfo);
            _inputData.IsTouching = true;
        }

        if (Input.GetMouseButton(0))
        {
            EmitRay(out _inputData.TouchInfo);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _inputData.IsTouching = false;
        }
    }

    private void EmitRay(out RaycastHit touchInfo)
    {
        Physics.Raycast(_sceneData.Camera.ScreenPointToRay(Input.mousePosition), out touchInfo);
    }
}
