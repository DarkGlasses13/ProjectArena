using UnityEngine;

[CreateAssetMenu(menuName = "Create static data", fileName = "New static data", order = 1)]

public class StaticData : ScriptableObject
{
    public GameObject BouncerPrefab;
    public float BouncerMoveSpeed;
    public int Clickabel;
}
