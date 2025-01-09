using UnityEngine;
using UnityEngine.UI;
using System;

namespace View
{
    public class GameView : MonoBehaviour
    {
        [Header("Game View Canvas")] 
        [SerializeField] private GameObject startCanvas;
        [SerializeField] private GameObject gameCanvas;
        [SerializeField] private GameObject healthPlayerCanvas;
        
        [Header("Game View panel")] 
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject gamePanel;
        
        [Header("Button Restart")]
        [SerializeField] private Button[] buttonsRestart;
        public event Action OnRestartButtonPressed;
        
        public void Initialize()
        {
            SetUIState(false, false, false);
            gameCanvas.SetActive(false);
            startCanvas.SetActive(true);

            foreach (var button in buttonsRestart)
            {
                button.onClick.AddListener(() => OnRestartButtonPressed?.Invoke());
            }
        }
        
        public void StartGame(bool isGameRun)
        {
            gameCanvas.SetActive(isGameRun);
            startCanvas.SetActive(!isGameRun);
            SetUIState(isGameRun, false, false);
        }

        public void EndGame(bool isWin)
        {
            SetUIState(false, isWin, !isWin);
        }
        
        private void SetUIState(bool isGameActive, bool showWin, bool showLose)
        {
            healthPlayerCanvas.SetActive(isGameActive);
            gamePanel.SetActive(isGameActive);
            winPanel.SetActive(showWin);
            losePanel.SetActive(showLose);
        }
    }
}