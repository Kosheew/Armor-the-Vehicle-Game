using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;

        public void SetHealth(int health)
        {
            healthSlider.maxValue = health;
            healthSlider.value = health;
        }

        public void UpdateHealth(int health)
        {
            healthSlider.value = health;
        }
    }
}
