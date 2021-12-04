using UnityEngine;
using Leopotam.Ecs;

public class HideJoysticSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<Mover> _moverFilter;

    public void Run()
    {
        if (_moverFilter.IsEmpty() && Input.GetMouseButtonUp(0))
        {
            _sceneData.MovePanel.SetActive(false);
        }

        if (_moverFilter.IsEmpty() == false)
        {
            _sceneData.MovePanel.SetActive(true);
        }
    }
}
