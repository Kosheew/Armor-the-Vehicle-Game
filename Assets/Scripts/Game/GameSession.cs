using Character;
using Handlers.Touch;
using Timer;
using Character.Car;
using UnityEngine;
using View;
using UnityEngine.SceneManagement;
using System;
using Handlers.Enemies;

namespace Game
{
    public class GameSession : IUpdatable
    {
        public event Action OnGameStart;
        public event Action<bool> OnGameEnd;
        
        public bool IsGameRun { get; private set; }
        public bool IsGameOver { get; private set; }
        public bool IsWin { get; private set; }
        
        private TimerModel _timer;
        private EnemyManager _enemyManager;
        private ITouchHandler _touchHandler;
        private ITargetHandler _targetHandler;

        public void Inject(DependencyContainer container)
        {
            _touchHandler = container.Resolve<ITouchHandler>();
            _targetHandler = container.Resolve<ITargetHandler>();
            _enemyManager = container.Resolve<EnemyManager>();
            _timer = container.Resolve<TimerModel>();

            _timer.OnTimerCompleted += HandleTimerComplete;
        }

        public void InitializeGame()
        {
            IsGameRun = false;
            IsWin = false;
            IsGameOver = false;
        }

        public void UpdateObject()
        {
            if (!IsGameRun && !IsGameOver)
            {
                TryStartGame();
            }
            
            if(IsGameRun)
            {
                _timer.UpdateTimer(Time.deltaTime);
                UpdateGameLogic();
            }
        }

        private void TryStartGame()
        {
            if (_touchHandler.IsTouchActive())
            {
                IsGameRun = true;
                OnGameStart?.Invoke();
                _timer.StartTimer();
            }
        }

        private void UpdateGameLogic()
        {
            if (!_targetHandler.TargetAlive)
            {
                EndGame(false);
            }
        }

        private void HandleTimerComplete()
        {
            if (IsGameRun && !IsGameOver)
            {
                EndGame(true);
            }
        }

        private void EndGame(bool isWin)
        {
            IsGameOver = true;
            IsGameRun = false;
            IsWin = isWin;
            OnGameEnd?.Invoke(isWin);
            _timer.StopTimer();
            _enemyManager.StopAllEnemies();
        }

        public void RestartGame()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}
