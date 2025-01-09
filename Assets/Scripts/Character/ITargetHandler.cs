using UnityEngine;

namespace Character
{
    public interface ITargetHandler
    {
        public bool TargetAlive { get;}
        Transform TargetPosition { get; }
    }
}