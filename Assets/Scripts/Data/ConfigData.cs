using UnityEngine;

[CreateAssetMenu(menuName = "Create Configuration", fileName = "New Configuration", order = 1)]

public class ConfigData : ScriptableObject
{
    private const int _bouncerMoveSpeedPercentModifire = 10;
    private const int _monsterMoveSpeedPercentModifire = 20;
    private const int _throwForcePercentModifire = 3;

    [HideInInspector] public float RotationSmooth = 3;

    [Header("CONTROL")]
    public MovementType MovementType;

    [Header("[BALANCE SETTINGS]")]
    public int StartMonsterPoolSize;
    public SpawnMultiplier MonsterSpawnMultiplier;
    [Range(1, 10)] public int MaxTimeBetweenMonsterSpawn;
    [Range(1, 10)] public int GeneratorHelth;
    [SerializeField] [Range(0, 100)] private float _startBouncerMoveSpeed;
    [SerializeField] [Range(0, 100)] private float _startBouncerThrowForce;
    [SerializeField] [Range(0, 100)] private float _defaultMonsterMoveSpeed;
    public float BouncerMoveSpeed { get { return _startBouncerMoveSpeed / _bouncerMoveSpeedPercentModifire; } }
    public float ThrowForce { get { return _startBouncerThrowForce / _throwForcePercentModifire; } }
    public float DefaultMonsterMoveSpeed { get { return _defaultMonsterMoveSpeed / _monsterMoveSpeedPercentModifire; } }

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

public enum SpawnMultiplier
{
    X1,
    X2,
    X3
}
