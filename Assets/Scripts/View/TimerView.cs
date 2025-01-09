using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class TimerView : MonoBehaviour
    { 
        [SerializeField] private Slider timerSlider;

        public void StartTimer(float targetSeconds)
        {
            timerSlider.maxValue = targetSeconds;
        }
         
        public void UpdateTimer(float seconds)
        {
            timerSlider.value = seconds;
        }
    }
}