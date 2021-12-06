using Leopotam.Ecs;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), (typeof(Collider)))]
public abstract class Transmitter : MonoBehaviour
{
    public EcsEntity Entity;
}
