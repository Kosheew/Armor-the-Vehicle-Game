using UnityEngine;
using ObjectPool;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private float cooldown = 0.5f;
    [SerializeField] private float turretSpeed = 15f;
    
    private Camera _mainCamera;
    
    private float cooldownCounter = 0;
    private CustomPool<Bullet> _bulletPool;

    public void Inject(DependencyContainer container)
    {
        _bulletPool = container.Resolve<CustomPool<Bullet>>();
        
        _mainCamera = Camera.main;
    }
    public void UpdateState()
    {
        cooldownCounter += Time.deltaTime;
        if (cooldownCounter >= cooldown)
        {
            Shoot();
            cooldownCounter = 0;
        }
        
        if (Input.touchCount > 0) 
        {
            RotateTurretToTouch(Input.GetTouch(0));
        }
    }

    private void RotateTurretToTouch(Touch touch)
    {
        Ray ray = _mainCamera.ScreenPointToRay(touch.position);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 direction = (hitInfo.point - transform.position).normalized;
            direction.y = 90; 

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turretSpeed * Time.deltaTime);
            }
        }
    }
    
    private void Shoot()
    {
        Bullet bullet = _bulletPool.Get();
        bullet.transform.position = muzzlePoint.position;
        bullet?.Launch(muzzlePoint.forward);
    }

}
