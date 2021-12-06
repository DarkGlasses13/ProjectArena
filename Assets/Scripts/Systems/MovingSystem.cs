using UnityEngine;
using Leopotam.Ecs;

class MovingSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private InputData _inputData;
    private EcsFilter<Mover> _moverFilter;

    public void Run()
    {
        switch (_inputData.JoysticSide)
        {
            case JoysticSide.Stand:
                return;

            case JoysticSide.Left:
                Move(_sceneData.LeftMovableEdge);
                break;

            case JoysticSide.Right:
                Move(_sceneData.RightMovableEdge);
                break;
        }
    }

    private void Move(Transform target)
    {
        foreach (int moverIndex in _moverFilter)
        {
            ref Mover mover = ref _moverFilter.Get1(moverIndex);

            mover.Vew.transform.position = Vector3.MoveTowards
            (
                mover.Vew.transform.position,
                target.position,
                (mover.MoveSpeed * _inputData.JoysticDeflection) * Time.deltaTime
            );
        }
    }
}
