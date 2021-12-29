using UnityEngine;

[CreateAssetMenu(menuName = "Create Configuration", fileName = "New Configuration", order = 1)]

public class ConfigData : ScriptableObject
{

    [HideInInspector] public float RotationSmooth = 3;

    [Header("CONTROL")]
    public MovementType MovementType;

    [Header("[BALANCE SETTINGS]")]
    public int RobotPoolSize;
    public SpawnMultiplier RobotSpawnMultiplier;
    public float MaxTimeBetweenRobotSpawn = 10;
    [Range(1, 10)] public int GeneratorHelth;

    [SerializeField] [Range(0, 100)] private float _startBouncerMoveSpeed;
    [SerializeField] [Range(0, 100)] private float _startBouncerThrowForce;
    [SerializeField] [Range(0, 100)] private float _defaultRobotMoveSpeed;

    public float BouncerMoveSpeed { get { return _startBouncerMoveSpeed / _bouncerMoveSpeedPercentModifire; } }
    public float ThrowForce { get { return _startBouncerThrowForce / _throwForcePercentModifire; } }
    public float DefaultMonsterMoveSpeed { get { return _defaultRobotMoveSpeed / _robotMoveSpeedPercentModifire; } }

    [Header("[PREFABS]")]
    public GameObject PlayerPrefab;
    public GameObject ProjectilePrefab;
    public GameObject GeneratorPrefab;
    public GameObject DefaultRobotPrefab;

    [Header("[PHYSICAL LAYERS]")]
    public int FieldLayer;
    public int WallLayer;
    public int BouncerLayer;
    public int ProjectileLayer;
    public int RobotLayer;
    public int GeneratorLayer;

    [Header("[RICOCHET TRAJECTORY]")]
    [Range(3, 50)]public float TrajectoryLength;
    [Range(1, 10)]public int ReflectionsCount;

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