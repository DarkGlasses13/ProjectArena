using Leopotam.Ecs;
using UnityEngine;

public class ButtonInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private StaticData _staticData;
    private SceneData _sceneData;

    public void Init()
    {
        InitMoveButton(MoveSide.Left, _sceneData.LeftMoveButton);
        InitMoveButton(MoveSide.Right, _sceneData.RightMoveButton);
    }

    private void InitButton(GameObject buttonVew, out EcsEntity buttonEntity)
    {
        buttonEntity = _world.NewEntity();
        ref Button buttonComponent = ref buttonEntity.Get<Button>();
        ref Clickable clickableComponent = ref buttonEntity.Get<Clickable>();
        ClickHandler clickHandler = buttonVew.AddComponent<ClickHandler>();
        clickHandler.Entity = buttonEntity;
        buttonComponent.Vew = buttonVew;
        buttonComponent.Vew.layer = _staticData.Clickabel;
    }

    private MoveButton InitMoveButton(MoveSide moveSide, GameObject buttonVew)
    {
        InitButton(buttonVew, out EcsEntity buttonEntity);
        ref MoveButton moveButtonComponent = ref buttonEntity.Get<MoveButton>();
        moveButtonComponent.MoveSide = moveSide;
        return moveButtonComponent;
    }
}
