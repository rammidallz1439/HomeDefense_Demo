using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [Header("GameObjects")]
    public Transform EnemySpawnPoint;
    public Transform Platform;

    [Space(10)]
    [Header("Data Structures")]
    public List<EnemyScriptable> Enemies;

    [Space(10)]
    [Header("Data")]
    public float CoolDownTime;
    public float SpawnRate;
}
