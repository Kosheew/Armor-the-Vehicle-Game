using UnityEngine;

namespace Handlers.Animation
{
    public class SpeedAnimationHandler: IAnimationHandler<float>, IAnimationResetHandler
    {
        private readonly int _animIdSpeed = Animator.StringToHash("Speed");

        public void UpdateAnimation(Animator animator, float speed)
        {
            animator.SetFloat(_animIdSpeed, speed);
        }

        public void ResetAnimation(Animator animator)
        {
            animator.SetFloat(_animIdSpeed, 0);
        }
    }
}