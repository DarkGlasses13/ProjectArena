using Leopotam.Ecs;
using UnityEngine;

public class CatchTransmitter : Transmitter
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<ShotTransmitter>(out ShotTransmitter shotTransmitter))
        {
            Entity.Get<ThrowReady>().ThrowableProjectile = shotTransmitter.Entity;
            shotTransmitter.Entity.Get<CatchTrigger>();
        }
    }
}
