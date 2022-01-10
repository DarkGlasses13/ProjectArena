using Leopotam.Ecs;
using UnityEngine;

public class RicochetSystem : IEcsRunSystem
{
    private EcsFilter<Projectile, Vew>.Exclude<Caught> _projectileFilter;

    public void Run()
    {
        foreach (int index in _projectileFilter)
        {
            ref EcsEntity projectile = ref _projectileFilter.GetEntity(index);
            ref Projectile peojectileComponent = ref projectile.Get<Projectile>();
            ref Vew vewComponent = ref projectile.Get<Vew>();
            Vector3 rayOffset = new Vector3(vewComponent.Object.transform.position.x, 0.6f, vewComponent.Object.transform.position.z);
            Ray ray = new Ray(rayOffset, vewComponent.Object.transform.forward);
            float rayLength = 0.5f;

            if (Physics.Raycast(ray, out peojectileComponent.hitInfo, rayLength))
            {
                Vector3 reflectDirection = Vector3.Reflect(ray.direction, peojectileComponent.hitInfo.normal);
                float rotation = 90 - Mathf.Atan2(reflectDirection.z, reflectDirection.x) * Mathf.Rad2Deg;
                vewComponent.Object.transform.eulerAngles = new Vector3(0, rotation, 0);

                if (peojectileComponent.hitInfo.collider.TryGetComponent(out Transmitter transmitter))
                {
                    transmitter.Entity.Get<HitTrigger>();
                }
            }
        }
    }
}
