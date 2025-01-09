using System.Collections.Generic;
using Character.Enemy;
using ObjectPool;
using UnityEngine;

namespace Handlers.Enemies
{
    public class EnemySpawner
    {
        private CustomPool<Enemy> _enemyPool;
        private List<Enemy> _enemies;

        private float _spawnDelay;
        
        public void Inject(DependencyContainer container)
        {
            _enemyPool = container.Resolve<CustomPool<Enemy>>();
            _enemies = new List<Enemy>(_enemyPool.PoolSize);
        }

        public void InitialSpawn(Transform[] spawnPoints)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                var enemy = _enemyPool.Get();
                enemy.transform.position = spawnPoint.position;
                _enemies.Add(enemy);
            }
        }

        public void EnemyInject(DependencyContainer container)
        {
            foreach (var enemy in _enemies)
            {
                enemy.Inject(container);
            }
        }

        public void SpawnEnemiesUpdate(Transform[] spawnPoints, float spawnInterval)
        {
            _spawnDelay += Time.deltaTime;
            if (_spawnDelay >= spawnInterval)
            {
                if (_enemyPool.CountInactive > 0)
                {
                    var enemy = _enemyPool.Get();
                    enemy.ResetEnemy();
                    enemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                }

                _spawnDelay = 0;
            }
        }

        public void UpdateEnemies()
        {
            foreach (var enemy in _enemies)
            {
                if (enemy.isActiveAndEnabled)
                    enemy.UpdateObject();
            }
        }

        public void StopAllEnemies()
        {
            foreach (var enemy in _enemies)
            {
                enemy.ResetEnemy();
            }
        }
    }
}