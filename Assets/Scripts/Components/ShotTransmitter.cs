using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Actions;
using UnityEngine;

public class ShotTransmitter : Transmitter
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<CatchTransmitter>(out CatchTransmitter catchTransmitter))
        {
            Entity.Get<Caught>().Catcher = catchTransmitter.Entity;
        }

        //if (collision.gameObject.TryGetComponent<KnockOutHandler>(out KnockOutHandler knockOutHandler))
        //{
        //    // Логика попадания в монстра
        //    // От монстров снаряд отскакивает
        //}
    }
}
