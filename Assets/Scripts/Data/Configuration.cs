using UnityEngine;

[CreateAssetMenu(menuName = "Create Configuration", fileName = "New Configuration", order = 1)]

public class Configuration : ScriptableObject
{
    public GameObject PlayerPrefab;
    [Range(0, 50)] public float PlayerMoveSpeed;
    [Range(0, 50)] public float StartThrowForce;
    public GameObject ProjectilePrefab;
    public int BouncerLayer;
    public int ProjectileLayer;
    public int ClickableLayer;
}
