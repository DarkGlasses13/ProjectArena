using Leopotam.Ecs;
using UnityEngine;

public class ButtonInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private Configuration _staticData;
    private SceneData _sceneData;

    public void Init()
    {
        InitMoveButton(MoveSide.Left, _sceneData.LeftMoveButton);
        InitMoveButton(MoveSide.Right, _sceneData.RightMoveButton);
    }

    private void InitButton(GameObject buttonVew)
    {
        EcsEntity buttonEntity = _world.NewEntity();
        EntityComponentAdder.AddButton(buttonEntity, buttonVew, _staticData.ClickableLayer);
    }

    private void InitMoveButton(MoveSide moveSide, GameObject buttonVew)
    {
        EcsEntity buttonEntity = _world.NewEntity();
        EntityComponentAdder.AddMoveButton(buttonEntity, buttonVew, _staticData.ClickableLayer, moveSide);
    }
}
