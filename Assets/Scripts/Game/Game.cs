using Character;
using Character.Car;
using Character.Enemy;
using UnityEngine;
using ObjectPool;
using Handlers.Enemies;
using Handlers.Animation;
using Handlers.Touch;
using Timer;
using View;
using Turrets;
using Camera_Controllers;
using Presenters;

namespace Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        
        [SerializeField] private TimerView timerView;
        [SerializeField] private GameView gameView;

        [SerializeField] private CameraMove cameraMove;

        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Enemy enemyPrefab;
        
        [SerializeField] private Turret turret;
        [SerializeField] private Car car;

        [SerializeField] private Ground ground;
        [SerializeField] private EnemyManager enemyManager;

        private CustomPool<Bullet> _bulletPool;
        private CustomPool<Enemy> _enemyPool;

        private DependencyContainer _dependencyContainer;

        private DamageAnimationHandler _damageAnimationHandler;
        private SpeedAnimationHandler _speedAnimationHandler;

        private EnemySpawner _enemySpawner;

        private TouchInputHandler _touchInputHandler;
        
        private GameSession _gameSession;
        private GameSessionPresenter _gameSessionPresenter;

        private TimerModel _timerModel;
        private TimerController _timerController;

        private void Awake()
        {
            Application.targetFrameRate = gameSettings.FrameRate;
            
            InstantiateObjects();

            Register();

            Inject();
        }

        private void Update()
        {
            _gameSession.UpdateObject();

            if (_gameSession.IsGameRun && car.TargetAlive)
            {
                turret.UpdateObject();
                enemyManager.UpdateObject();
            }
        }

        private void LateUpdate()
        {
            if (car.TargetAlive && _gameSession.IsGameRun)
            {
                ground.UpdateObject();
                cameraMove.UpdateObject();
            }
        }

        private void Inject()
        {
            ground.Inject(_dependencyContainer);
            _timerController.Inject(_dependencyContainer);
            car.Inject(_dependencyContainer);
            turret.Inject(_dependencyContainer);
            _gameSession.Inject(_dependencyContainer);
            _enemySpawner.Inject(_dependencyContainer);
            enemyManager.Inject(_dependencyContainer);
            _gameSessionPresenter.Inject(_dependencyContainer);
            cameraMove.Inject(_dependencyContainer);
        }

        private void InstantiateObjects()
        {
            _dependencyContainer = new DependencyContainer();

            _touchInputHandler = new TouchInputHandler();

            _timerModel = new TimerModel(gameSettings.TimeGame);
            _timerController = new TimerController();

            _bulletPool = new CustomPool<Bullet>(bulletPrefab, gameSettings.MaxBullets);
            _enemyPool = new CustomPool<Enemy>(enemyPrefab, gameSettings.MaxEnemies);

            _damageAnimationHandler = new DamageAnimationHandler();
            _speedAnimationHandler = new SpeedAnimationHandler();

            _gameSessionPresenter = new GameSessionPresenter();
            _gameSession = new GameSession();
            _enemySpawner = new EnemySpawner();
        }

        private void Register()
        {
            _dependencyContainer.Register(gameSettings);
            _dependencyContainer.Register(_bulletPool);
            _dependencyContainer.Register(_enemyPool);
            _dependencyContainer.Register(_enemySpawner);
            _dependencyContainer.Register(_gameSession);
            _dependencyContainer.Register(gameView);

            _dependencyContainer.Register(_timerController);
            _dependencyContainer.Register(timerView);
            _dependencyContainer.Register(_timerModel);

            _dependencyContainer.Register<ITouchHandler>(_touchInputHandler);
            _dependencyContainer.Register<ITargetHandler>(car);
            _dependencyContainer.Register<IAnimationHandler<bool>>(_damageAnimationHandler);
            _dependencyContainer.Register<IAnimationHandler<float>>(_speedAnimationHandler);
            _dependencyContainer.Register<IAnimationResetHandler>(_speedAnimationHandler);
            _dependencyContainer.Register(enemyManager);
            
        }
    }
}
