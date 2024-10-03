using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyManager
{
    protected EnemyHandler Handler;

    #region EventHandlers
    protected void EnemySpawnEventHandler(EnemySpawnEvent e)
    {
        if(Handler.CoolDownTime <= 0)
        {
            foreach (EnemyData item in e.Wave.EnemyData)
            {
                if (item.TimeToStart <= 0 )
                {
                    SpawnEnemies(item.EnemyScriptable.EnemyPrefab,item);
                }
                else
                {
                    if (e.Timer <= item.TimeToStart)
                    {
                        SpawnEnemies(item.EnemyScriptable.EnemyPrefab,item);
                    }
                }
            }
            Handler.CoolDownTime = 1f/Handler.SpawnRate;
        }
        Handler.CoolDownTime -= Time.deltaTime;
    } 

    protected void EnemyMovementEventHandler(EnemyMovementEvent e)
    {
        e.Agent.SetDestination(Handler.House.position);
    }


    protected void FindTargetEventHandler(FindTargetEvent e)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(e.ShootingMachine.transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= e.ShootingMachine.TurretDataScriptable.Range)
        {
            e.ShootingMachine.Target = nearestEnemy.transform.GetComponent<Enemy>();
        }
    }
    #endregion

    #region Functions

    // Spawns diffrent enemies and keeps their Position inside the platform area
    void SpawnEnemies(GameObject enemyObject,EnemyData data)
    {
        Collider cubeCollider = Handler.Platform.GetComponent<Collider>();

        float cubeMinX = cubeCollider.bounds.min.x;
        float cubeMaxX = cubeCollider.bounds.max.x;
        float spawnZ = Handler.EnemySpawnPoint.position.z;

        float randomX = Random.Range(cubeMinX, cubeMaxX);

        Vector3 spawnPosition = new Vector3(randomX, Handler.EnemySpawnPoint.position.y, spawnZ);
        GameObject enemy = MonoHelper.Instance.InstantiateObject(enemyObject, spawnPosition, Quaternion.identity);
        enemy.transform.GetComponent<Enemy>().Health = data.EnemyScriptable.Health;

    }



    #endregion
}
