using UnityEngine;

[CreateAssetMenu(menuName = "Create Configuration", fileName = "New Configuration", order = 1)]

public class Configuration : ScriptableObject
{
    public PlatformType Platform;

    [Header("[BALANCE SETTINGS]")]
    public int StartMonsterPoolSize;
    public SpawnMultiplier MonsterSpawnMultiplier;

    [Header("[VALUES]")]
    [Range(0, 100)] public float BouncerMoveSpeed;
    [Range(0, 10)] public float RotationSmooth;
    [Range(0, 100)] public float StartBouncerThrowForce;

    [Header("[PREFABS]")]
    public GameObject PlayerPrefab;
    public GameObject ProjectilePrefab;
    public GameObject GeneratorPrefab;
    public GameObject DefaultMonsterPrefab;

    [Header("[PHYSICAL LAYERS]")]
    public int FieldLayer;
    public int WallLayer;
    public int BouncerLayer;
    public int ProjectileLayer;
    public int MonsterLayer;
    public int GeneratorLayer;
}

public enum PlatformType
{
    Mobile,
    PC
}

public enum SpawnMultiplier
{
    X1,
    X2,
    X3
}
