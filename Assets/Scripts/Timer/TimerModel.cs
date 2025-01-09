using System;

namespace Timer
{
     public class TimerModel
    {
        private bool _isRunning;
        private float _time = 0f;
        
        public float TargetTime { get; private set; }
        public event Action<float> OnTimerChanged;
        public event Action OnTimerCompleted;
        
        public TimerModel(float timeEndGame)
        {
            TargetTime = timeEndGame;
        }
        public void StartTimer()
        {
            _isRunning = true;
        }

        public void StopTimer()
        {
            _isRunning = false;
        }

        public void ResetTimer()
        {
            _time = 0f;
            OnTimerChanged?.Invoke(0);
        }

        public void UpdateTimer(float deltaTime)
        {
            if (!_isRunning) return;

            _time += deltaTime;
            OnTimerChanged?.Invoke(_time);
            
            if (_time >= TargetTime)
            {
                StopTimer();
                OnTimerCompleted?.Invoke();
            }
        }
    }
}