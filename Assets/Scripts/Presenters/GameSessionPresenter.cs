using System.Collections;
using System.Collections.Generic;
using View;
using Game;

namespace Presenters
{
    public class GameSessionPresenter
    {
        private GameSession _controller;
        private GameView _view;

        public void Inject(DependencyContainer container)
        {
            _controller = container.Resolve<GameSession>();
            _view = container.Resolve<GameView>();

            _controller.OnGameStart += HandleGameStart;
            _controller.OnGameEnd += HandleGameEnd;

            _view.OnRestartButtonPressed += HandleRestartButtonPressed;

            _view.Initialize();
            _controller.InitializeGame();
        }

        private void HandleGameStart()
        {
            _view.StartGame(true);
        }

        private void HandleGameEnd(bool isWin)
        {
            _view.EndGame(isWin);
        }

        private void HandleRestartButtonPressed()
        {
            _controller.RestartGame();
        }
    }
}