using UnityEngine;

namespace Camera_Controllers
{
    public class CameraMove : MonoBehaviour, IUpdatable
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform endPoint;
        
        private GameSettings _gameSettings;
        private bool _hasReachedTarget;

        public void Inject(DependencyContainer dependencyContainer)
        {
            _gameSettings = dependencyContainer.Resolve<GameSettings>();
        }
        
        public void UpdateObject()
        {
            if (_hasReachedTarget) return;

            CameraMovement();
        }

        private void CameraMovement()
        {
            if (target != null)
            {
                MoveTowardsEndPoint();
                RotateTowardsTarget();

                if (Vector3.Distance(transform.position, endPoint.position) < 0.1f)
                {
                    _hasReachedTarget = true;
                }
            }
        }
        
        private void MoveTowardsEndPoint()
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, _gameSettings.SpeedCamera * Time.deltaTime);
        }
        
        private void RotateTowardsTarget()
        {
            Vector3 directionToTarget = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            targetRotation *= Quaternion.Euler(-_gameSettings.CameraOffsetAngleX, 0f, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _gameSettings.SpeedCamera);
        }
    }
}
