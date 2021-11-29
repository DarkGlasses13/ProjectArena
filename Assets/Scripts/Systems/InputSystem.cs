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
            _inputData.IsTouching = true;
            EmitRay(out _inputData.TouchInfo);
            CallClickhandler(ClickhandlerCallType.Click);
        }

        if (Input.GetMouseButton(0))
        {
            EmitRay(out _inputData.TouchInfo);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _inputData.IsTouching = false;
            EmitRay(out _inputData.TouchInfo);
            CallClickhandler(ClickhandlerCallType.Unclick);
        }
    }

    private void EmitRay(out RaycastHit touchInfo)
    {
        Physics.Raycast(_sceneData.Camera.ScreenPointToRay(Input.mousePosition), out touchInfo);
    }

    private void CallClickhandler(ClickhandlerCallType callType)
    {
        switch (callType)
        {
            case ClickhandlerCallType.Click:
                Collider touchingObject = _inputData.TouchInfo.collider;

                if (touchingObject && touchingObject.TryGetComponent<ClickHandler>(out _inputData.CalledClickHandler))
                {
                    _inputData.CalledClickHandler.Click();
                }
                break;

            case ClickhandlerCallType.Unclick:
                if (_inputData.CalledClickHandler != null)
                {
                    _inputData.CalledClickHandler.Unclick();
                }
                break;
        }
    }

    private enum ClickhandlerCallType
    {
        Click,
        Unclick
    }
}
