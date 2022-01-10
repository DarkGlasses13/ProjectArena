using UnityEngine;

[CreateAssetMenu(menuName = "Create Configuration", fileName = "New Configuration", order = 1)]

public class ConfigData : ScriptableObject
{
    [HideInInspector] public float BouncerRotationSmooth = 3;

    [Header("CONTROL")]
    public MovementType MovementType;

    [Header("[BALANCE SETTINGS]")]
    public int EnemyPoolSize;
    public SpawnMultiplier EnemySpawnMultiplier;
    public float MaxTimeBetweenEnemySpawn = 10;
    [Range(1, 10)] public int ServerHelth;

    [SerializeField] [Range(0, 100)] private float _startBouncerMoveSpeed;
    [SerializeField] [Range(0, 100)] private float _startProjectileSpeed;
    [SerializeField] [Range(0, 100)] private float _defaultRobotMoveSpeed;

    public float BouncerMoveSpeed { get { return _startBouncerMoveSpeed / _bouncerMoveSpeedPercentModifire; } }
    public float StartProjectileSpeed { get { return _startProjectileSpeed / _throwForcePercentModifire; } }
    public float DefaultEnemyMoveSpeed { get { return _defaultRobotMoveSpeed / _robotMoveSpeedPercentModifire; } }

    [Header("[PREFABS]")]
    public GameObject PlayerPrefab;
    public GameObject ProjectilePrefab;
    public GameObject ServerPrefab;
    public GameObject DefaultEnemyPrefab;

    [Header("[PHYSICAL LAYERS]")]
    public int FieldLayer;
    public int WallLayer;
    public int BouncerLayer;
    public int ProjectileLayer;
    public int EnemyLayer;
    public int ServerLayer;

    [Header("[RICOCHET TRAJECTORY]")]
    public GameObject TrajectoryLine;
    public GameObject EndTrajectoryLine;
    [Range(3, 50)]public float StartTrajectoryLength;
    [Range(1, 10)]public int StartReflectionsCount;

    private const int _bouncerMoveSpeedPercentModifire = 10;
    private const int _robotMoveSpeedPercentModifire = 20;
    private const int _throwForcePercentModifire = 3;
}

public enum SpawnMultiplier
{
    X1,
    X2,
    X3
}