using UnityEngine;

namespace Handlers.Touch
{
    public class TouchInputHandler: ITouchHandler
    {
        public Vector2 GetTouchPosition()
        {
            if (Input.touchCount > 0)
            {
                return Input.GetTouch(0).position;
            }
            return Vector2.zero;
        }

        public bool IsTouchActive()
        {
            return Input.touchCount > 0;
        }
    }
}