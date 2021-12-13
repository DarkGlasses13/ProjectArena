using Leopotam.Ecs;
using UnityEngine;

public class Transmitter : MonoBehaviour
{
    public TransmitterType Type;
    public EcsEntity Entity;
}

public enum TransmitterType
{
    Bouncer,
    Projectile,
    Generator,
    Monster
}
