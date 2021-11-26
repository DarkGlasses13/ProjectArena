using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

class MoveControlSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;
    private SceneData _sceneData;
    private InputData _inputData;
    private MoveButton _leftButton;
    private MoveButton _rightButton;
    private EcsFilter<Mover> _moverFilter;
    private List<Mover> _movers = new List<Mover>();

    public void Init()
    {
        _leftButton = InitButton(MoveSide.Left, _sceneData.LeftMoveButton);
        _rightButton = InitButton(MoveSide.Right, _sceneData.RightMoveButton);

        foreach (var entityIndex in _moverFilter)
        {
            _movers.Add(_moverFilter.Get1(entityIndex));
        }
    }

    public void Run()
    {
        if (_leftButton.IsClicked)
        {
            Move(_sceneData.LeftMovableEdge.position);
        }

        if (_rightButton.IsClicked)
        {
            Move(_sceneData.RightMovableEdge.position);
        }
    }

    private void Move(Vector3 target)
    {
        foreach (Mover mover in _movers)
        {
            mover.MovableObject.transform.position = Vector3.MoveTowards(mover.MovableObject.transform.position, target, mover.MoveSpeed * Time.deltaTime);
        }
    }

    private MoveButton InitButton(MoveSide moveSide, GameObject button)
    {
        EcsEntity buttonEntity = _world.NewEntity();
        ref MoveButton moveButton = ref buttonEntity.Get<MoveButton>();
        moveButton.MoveSide = moveSide;
        moveButton.Button = button;
        return moveButton;
    }
}
