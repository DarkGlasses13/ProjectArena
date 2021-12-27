using Leopotam.Ecs;
using UnityEngine;

public class InputSystem : IEcsRunSystem
{
    private ConfigData _configData;
    private SceneData _sceneData;
    private InputData _inputData;

    public void Run()
    {
        switch (_configData.MovementType)
        {
            case MovementType.Horizontal:
                _inputData.ControllerDirection = _sceneData.Joystick.Direction;
                break;
            case MovementType.Vertical:
                _inputData.ControllerDirection = _sceneData.Joystick.Direction;
                break;
            case MovementType.CombinedStatic:
                _inputData.ControllerDirection = _sceneData.Joystick.Direction;
                break;
            case MovementType.CombinedFree:
                _inputData.ControllerDirection = _sceneData.Joystick.Direction;
                break;
            case MovementType.MouseKeyboard:
                _inputData.ControllerDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                break;
        }
    }
}

public enum MovementType
{
    Horizontal,
    Vertical,
    CombinedStatic,
    CombinedFree,
    MouseKeyboard
}
