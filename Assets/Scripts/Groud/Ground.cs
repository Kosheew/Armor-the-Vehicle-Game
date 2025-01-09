using UnityEngine;

public class Ground : MonoBehaviour, IUpdatable
{
    private GameSettings _gameSettings;
    
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    
    public void Inject(DependencyContainer container)
    {
        _gameSettings = container.Resolve<GameSettings>();
        
        _startPosition = transform.position;
        _targetPosition = _startPosition + Vector3.back * _gameSettings.OffsetGround;
    }

    public void UpdateObject()
    {
        MoveGround();
    }

    private void MoveGround()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _gameSettings.SpeedGround * Time.deltaTime);
        if (transform.position.z <= _targetPosition.z)
        {
            transform.position = _startPosition;
        }
    }
}
