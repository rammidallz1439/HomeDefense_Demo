using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Vault;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private NavMeshAgent _agent;

    private void Start()
    {
        _agent.speed = _speed;
    }
    private void Update()
    {
        EventManager.Instance.TriggerEvent(new EnemyMovementEvent(_agent));
    }
}
