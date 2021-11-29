using Leopotam.Ecs;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public EcsEntity Entity;
    public EcsFilter<Clicked> ClickedFilter;

    public void Click()
    {
        Entity.Get<Clicked>();

    }

    public void Unclick()
    {
        Entity.Del<Clicked>();
    }
}
