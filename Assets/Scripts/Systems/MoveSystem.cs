using UnityEngine;
using Leopotam.Ecs;

class MoveSystem : IEcsRunSystem
{
    private EcsWorld _world;
    private SceneData _sceneData;
    private InputData _inputData;
    private EcsFilter<Mover> _moverFilter;

    public void Run()
    {
        if (GetButton(_sceneData.LeftMoveButton).IsPushed)
        {
            Move(_sceneData.LeftMovableEdge.position);
        }

        if (GetButton(_sceneData.RightMoveButton).IsPushed)
        {
            Move(_sceneData.RightMovableEdge.position);
        }
    }

    private void Move(Vector3 target)
    {
        foreach (int moverIndex in _moverFilter)
        {
            ref Mover mover = ref _moverFilter.Get1(moverIndex);
            mover.MovableObject.transform.position = Vector3.MoveTowards(mover.MovableObject.transform.position, target, mover.MoveSpeed * Time.deltaTime);
        }
    }

    private ref Button GetButton(GameObject buttonVew)
    {
        return ref buttonVew.GetComponent<ClickHandler>().Entity.Get<Button>();
    }
}
