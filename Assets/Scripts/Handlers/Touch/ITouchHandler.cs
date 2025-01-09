using UnityEngine;

namespace Handlers.Touch
{
    public interface ITouchHandler
    {
       public Vector2 GetTouchPosition();
       public bool IsTouchActive();
    }
}