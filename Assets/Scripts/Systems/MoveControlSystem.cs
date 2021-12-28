using UnityEngine;
using Leopotam.Ecs;

class MoveControlSystem : IEcsRunSystem
{
    private InputData _inputData;
    private EcsFilter<Mover, Vew> _moverFilter;

    public void Run()
    {
        foreach (int index in _moverFilter)
        {
            ref Mover moverComponent = ref _moverFilter.Get1(index);
            ref Vew vewComponent = ref _moverFilter.Get2(index);

            Vector3 moveDirection = new Vector3(_inputData.MotionInput.x, 0, _inputData.MotionInput.y);
            moverComponent.Controler.Move(moveDirection * moverComponent.MoveSpeed * Time.deltaTime);
        }
    }
}
