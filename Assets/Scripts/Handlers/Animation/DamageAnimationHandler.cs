using UnityEngine;

namespace Handlers.Animation
{
    public class DamageAnimationHandler: IAnimationHandler<bool>
    {
        private readonly int _animIdTakeDamage = Animator.StringToHash("TakeDamage");

        public void UpdateAnimation(Animator animator, bool takeDamage)
        {
            if (takeDamage)
            {
                animator.SetTrigger(_animIdTakeDamage);
            }
        }
    }
}