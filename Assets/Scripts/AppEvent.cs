using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Vault;

public class AppEvent
{
}


public struct EnemySpawnEvent : GameEvent
{
    public Wave Wave;
    public float Timer;

    public EnemySpawnEvent(Wave wave, float timer)
    {
        Wave = wave;
        Timer = timer;
    }
}

public struct IntialLevelSetUpEvent : GameEvent
{

}

public struct EnemyMovementEvent : GameEvent
{
    public NavMeshAgent Agent;

    public EnemyMovementEvent(NavMeshAgent agent)
    {
        Agent = agent;
    }
}