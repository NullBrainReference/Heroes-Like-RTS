using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private Vector3 _targetPosition;

    private void Awake()
    {
        _targetPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Move(MapInputsManager.Instance.MovementVector.normalized);
    }

    private void Move(Vector2 dir)
    {
        _targetPosition += new Vector3(dir.x, dir.y, 0) * _speed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, _targetPosition, 0.1f);
    }   
}
