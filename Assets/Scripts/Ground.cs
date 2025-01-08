using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float offset;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    
    public void Init()
    {
        _startPosition = transform.position;
        _targetPosition = _startPosition + Vector3.back * offset;
    }

    public void UpdateState()
    {
        MoveGround();
    }

    private void MoveGround()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
        if (transform.position.z <= _targetPosition.z)
        {
            transform.position = _startPosition;
        }
    }
}
