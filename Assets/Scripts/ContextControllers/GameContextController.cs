using Vault;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContextController : Registerer
{
    [SerializeField] private EnemyHandler _enemyHandler;
    [SerializeField] private LevelHandler _levelHandler;
    public override void Enable()
    {
    }

    public override void OnAwake()
    {
        AddController(new EnemyController(_enemyHandler));
        AddController(new LevelController(_levelHandler));
    }

    public override void OnStart()
    {
    }
}
