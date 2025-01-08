using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

public class Game : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Enemy enemyPrefab;
    
    [SerializeField] private int maxEnemies;
    [SerializeField] private int maxBullets;
    
    [SerializeField] private Turret turret;
    [SerializeField] private Car car;
    
    [SerializeField] private Ground ground;
    [SerializeField] private EnemySpawner spawner;
    
    private CustomPool<Bullet> _bulletPool;
    private CustomPool<Enemy> _enemyPool;
    
    private DependencyContainer _dependencyContainer;
    
    private void Awake()
    {
        _dependencyContainer = new DependencyContainer();
        
        InstantiatePool();
        
        Register();
        
        Inject();
    }
    
    private void Update()
    {
        turret.UpdateState();
    }

    private void LateUpdate()
    {
        ground.UpdateState();
    }
    
    private void Inject()
    {
        ground.Init();
        turret.Inject(_dependencyContainer);
        spawner.Inject(_dependencyContainer);
    }

    private void InstantiatePool()
    {
        _bulletPool = new CustomPool<Bullet>(bulletPrefab, maxBullets);
        _enemyPool = new CustomPool<Enemy>(enemyPrefab, maxEnemies);
    }

    private void Register()
    {
        _dependencyContainer.Register(_bulletPool);
        _dependencyContainer.Register(_enemyPool);
    }
}
