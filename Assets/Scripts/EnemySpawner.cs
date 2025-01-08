using System;
using System.Collections;
using System.Collections.Generic;
using ObjectPool;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 5f;
    
    private CustomPool<Enemy> enemyPool;
    
    public void Inject(DependencyContainer container)
    {
        enemyPool = container.Resolve<CustomPool<Enemy>>();
        InitialSpawn();
        StartCoroutine(SpawnEnemiesCoroutine());
    }
    
    private void InitialSpawn()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            if (enemyPool.CountInactive > 0)
            {
                SpawnEnemy(spawnPoint);
            }
        }
    }

    
    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (true) 
        {
            foreach (var spawnPoint in spawnPoints)
            {
                if (enemyPool.CountInactive > 0)
                {
                    SpawnEnemy(spawnPoint);
                }
                else
                {
                    Debug.Log("No available enemies in the pool!");
                }
            }
            
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    private void SpawnEnemy(Transform spawnPoint)
    {
        var enemy = enemyPool.Get();
        enemy.ResetEnemy();
        enemy.transform.position = spawnPoint.position;
    }

}
