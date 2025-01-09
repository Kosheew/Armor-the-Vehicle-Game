using System.Linq;
using UnityEngine;

namespace Handlers.Enemies
{
    public class EnemyManager : MonoBehaviour, IUpdatable
    {
        [SerializeField] private Transform[] spawnPoints;
        
        private GameSettings _gameSettings;
        private EnemySpawner _enemySpawner;
        
        public void Inject(DependencyContainer container)
        {
            _enemySpawner = container.Resolve<EnemySpawner>();
            _gameSettings = container.Resolve<GameSettings>();
            spawnPoints = GetComponentsInChildren<Transform>().Where(t => t != transform).ToArray();

            _enemySpawner.InitialSpawn(spawnPoints);
            _enemySpawner.EnemyInject(container);
        }
        
        public void UpdateObject()
        {
            _enemySpawner.UpdateEnemies();
            _enemySpawner.SpawnEnemiesUpdate(spawnPoints, _gameSettings.SpawnIntervalEnemy);
        }
        
        public void StopAllEnemies()
        {
            _enemySpawner.StopAllEnemies();
        }
    }
}