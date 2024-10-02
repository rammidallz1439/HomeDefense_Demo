using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    SpawnEnemies(item.EnemyScriptable.EnemyPrefab);
                }
                else
                {
                    if (e.Timer <= item.TimeToStart)
                    {
                        SpawnEnemies(item.EnemyScriptable.EnemyPrefab);
                    }
                }
            }
            Handler.CoolDownTime = 1f/Handler.SpawnRate;
        }
        Handler.CoolDownTime -= Time.deltaTime;
    } 

    #endregion

    #region Functions

    void SpawnEnemies(GameObject enemyObject)
    {
        Collider cubeCollider = Handler.Platform.GetComponent<Collider>();

        float cubeMinX = cubeCollider.bounds.min.x;
        float cubeMaxX = cubeCollider.bounds.max.x;
        float spawnZ = Handler.EnemySpawnPoint.position.z;

        float randomX = Random.Range(cubeMinX, cubeMaxX);

        Vector3 spawnPosition = new Vector3(randomX, Handler.EnemySpawnPoint.position.y, spawnZ);
        MonoHelper.Instance.InstantiateObject(enemyObject, spawnPosition, Quaternion.identity);

    }
    #endregion
}
