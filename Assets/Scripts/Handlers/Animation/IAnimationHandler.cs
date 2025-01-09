using UnityEngine;

namespace Handlers.Animation
{
    public interface IAnimationHandler<T>
    {
        public void UpdateAnimation(Animator animator, T context);
    }
}