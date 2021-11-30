using Leopotam.Ecs;
using UnityEngine;

public class ShotHandler : MonoBehaviour
{
    public Projectile Projectile;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<CatchHandler>(out CatchHandler catchHandler))
        {
            ref ThrowReady throwReady = ref catchHandler.Entity.Get<ThrowReady>();
            throwReady.Projectile = Projectile;
            Projectile.Rigidbody.gameObject.SetActive(false);
            Projectile.Rigidbody.transform.SetParent(catchHandler.transform);
            Projectile.Rigidbody.transform.localPosition = Vector3.zero;
        }

        //if (collision.gameObject.TryGetComponent<KnockOutHandler>(out KnockOutHandler knockOutHandler))
        //{
        //    // Логика попадания в монстра
        //}


    }
}
