using UnityEngine;

[CreateAssetMenu(menuName = "Create Configuration", fileName = "New Configuration", order = 1)]

public class Configuration : ScriptableObject
{
    public PlatformType Platform;
    public GameObject PlayerPrefab;
    public GameObject DefaultMonsterPrefab;
    [Range(0, 100)] public float BouncerMoveSpeed;
    [Range(0, 100)] public float StartBouncerThrowForce;
    public GameObject ProjectilePrefab;
    public int FieldLayer;
    public int WallLayer;
    public int BouncerLayer;
    public int ProjectileLayer;
    public int MonsterLayer;
}

public enum PlatformType
{
    Mobile,
    PC
}
