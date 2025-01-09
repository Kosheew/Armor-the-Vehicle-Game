using View;

namespace Timer
{
    public class TimerController
    {
        private TimerView _viewTimer;
        private TimerModel _timerModel;
        
        public void Inject(DependencyContainer container)
        {
            _viewTimer = container.Resolve<TimerView>();
            _timerModel = container.Resolve<TimerModel>();
            
            _timerModel.OnTimerChanged += UpdateView;
            
            _viewTimer.StartTimer(_timerModel.TargetTime);
            _timerModel.StartTimer();
        }

        private void UpdateView(float time)
        {
            _viewTimer.UpdateTimer(time);
        }
    }
}