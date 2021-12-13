using Leopotam.Ecs;
using UnityEngine;

public class JoysticInputSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private InputData _inputData;

    public void Run()
    {
        _inputData.JoysticDirection = _sceneData.Joystick.Direction;
    }
}

public enum MovementType
{
    Horizontal,
    Vertical,
    CombinedStatic,
    CombinedFree
}
