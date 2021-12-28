using Leopotam.Ecs;
using UnityEngine;

public class InputSystem : IEcsRunSystem
{
    private ConfigData _configData;
    private SceneData _sceneData;
    private InputData _inputData;

    public void Run()
    {
        GameObject virtualJoystic = _sceneData.VirtualJoystic.gameObject;

        switch (_configData.MovementType)
        {
            case MovementType.VirtualJoystick:
                _inputData.MotionInput = _sceneData.VirtualJoystic.Direction;
                if (virtualJoystic.activeSelf == false) { virtualJoystic.gameObject.SetActive(true); }
                break;

            case MovementType.MouseAndKeyboard:
                _inputData.MotionInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                if (virtualJoystic.activeSelf) { virtualJoystic.gameObject.SetActive(false); }
                break;
        }
    }
}

public enum MovementType
{
    VirtualJoystick,
    MouseAndKeyboard
}
