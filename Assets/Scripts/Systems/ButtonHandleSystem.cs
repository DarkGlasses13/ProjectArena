using Leopotam.Ecs;
using UnityEngine;

public class ButtonHandleSystem : IEcsRunSystem
{
    private EcsFilter<Button> _buttonFilter;
    private EcsFilter<Button, Clicked> _clickedButtonFilter;

    public void Run()
    {
        if (_clickedButtonFilter.IsEmpty())
        {
            foreach (int buttonIndex in _buttonFilter)
            {
                _buttonFilter.Get1(buttonIndex).IsPushed = false;
            }
        }
        else
        {
            foreach (int clicableButtonIndex in _clickedButtonFilter)
            {
                _clickedButtonFilter.Get1(clicableButtonIndex).IsPushed = true;
            }
        }
    }
}
