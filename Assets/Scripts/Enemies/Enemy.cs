using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Vault;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private NavMeshAgent _agent;
    public float Health;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Canvas _worldCanvas;
    private void Start()
    {
        _agent.speed = _speed;
        _healthBar.maxValue = Health;
        _healthBar.value = Health;
        _worldCanvas.worldCamera = Camera.main; 
    }
    private void Update()
    {
        MonoHelper.Instance.FaceCamera(Camera.main, _healthBar.transform);

        EventManager.Instance.TriggerEvent(new EnemyMovementEvent(_agent));
    }

    public void TakeDamage(float damage)
    {
        if (Health > 0)
        {
            Health -= damage;
            _healthBar.value = Health;
        }

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
