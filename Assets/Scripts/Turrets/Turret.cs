using Handlers.Touch;
using UnityEngine;
using ObjectPool;

namespace Turrets
{
    public class Turret : MonoBehaviour, IUpdatable
    {
        [SerializeField] private TurretSettings turretSettings;
        [SerializeField] private Transform muzzlePoint;

        private Camera _mainCamera;

        private float _cooldownCounter = 0;
        private CustomPool<Bullet> _bulletPool;

        private ITouchHandler _touchHandler;

        public void Inject(DependencyContainer container)
        {
            _bulletPool = container.Resolve<CustomPool<Bullet>>();
            _touchHandler = container.Resolve<ITouchHandler>();

            _mainCamera = Camera.main;
        }

        private void RotateTurretToTouch(Vector3 touchPosition)
        {
            touchPosition.y = 0;
            Ray ray = _mainCamera.ScreenPointToRay(touchPosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Vector3 targetPosition = hitInfo.point;
                targetPosition.y = transform.position.y;
                
                Vector3 direction = (targetPosition - transform.position).normalized;
                
                if (direction.sqrMagnitude > 0.01f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turretSettings.TurnSpeed * Time.deltaTime);
                }
            }
        }

        private void Shoot()
        {
            _cooldownCounter += Time.deltaTime;
            if (_cooldownCounter >= turretSettings.Cooldown)
            {
                Bullet bullet = _bulletPool.Get();
                bullet.transform.position = muzzlePoint.position;
                bullet?.Launch(muzzlePoint.forward);
                _cooldownCounter = 0;
            }
        }

        public void UpdateObject()
        {
            Shoot();

            if (_touchHandler.IsTouchActive())
            {
                Vector2 touchPosition = _touchHandler.GetTouchPosition();
                RotateTurretToTouch(touchPosition);
            }
        }
    }
}