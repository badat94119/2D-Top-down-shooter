using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private int _screenBorder;

    private Rigidbody2D _rigidbody;
    private Camera _camera;
    private Vector2 _targetDirection;
    private float _changeDirectionCooldown;
    private PlayerAwarenessController _awareOfPlayerController;
    private EnemyAttributes _enemyAttributes;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _awareOfPlayerController = GetComponent<PlayerAwarenessController>();
        _enemyAttributes = GetComponent<EnemyAttributes>();
        _camera = Camera.main;
        _targetDirection = Vector2.up;
    }
    
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void SetVelocity()
    {
        _rigidbody.velocity = transform.up * _enemyAttributes.Speed;
    }

    private void UpdateTargetDirection()
    {
        HandleRandomDirectionChange();
        HandlePlayerTargeting();
        HandleEnemyOffScreen();
    }

    private void HandlePlayerTargeting()
    {
        if (_awareOfPlayerController.AwareOfPlayer)
        {
            _targetDirection = _awareOfPlayerController.DirectionToPlayer;
        }
    }

    private void HandleRandomDirectionChange()
    {
        _changeDirectionCooldown -= Time.deltaTime;

        if (_changeDirectionCooldown <= 0)
        {
            float angleChange = Random.Range(-90, 90);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            _targetDirection = rotation * _targetDirection;

            _changeDirectionCooldown = Random.Range(1, 5);
        }
    }

    private void HandleEnemyOffScreen()
    {
        Vector2 screenPoint = _camera.WorldToScreenPoint(transform.position);

        if ((screenPoint.x < _screenBorder && _targetDirection.x < 0) ||
            (screenPoint.x > _camera.pixelWidth - _screenBorder && _targetDirection.x > 0))
        {
            _targetDirection = new Vector2(-_targetDirection.x, _targetDirection.y);
        }

        if ((screenPoint.y < _screenBorder && _targetDirection.y < 0) ||
            (screenPoint.y > _camera.pixelHeight - _screenBorder && _targetDirection.y > 0))
        {
            _targetDirection = new Vector2(_targetDirection.x, -_targetDirection.y);
        }
    }

    private void RotateTowardsTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        var rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _enemyAttributes.RotationSpeed * Time.deltaTime);

        _rigidbody.MoveRotation(rotation);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyMovement>())
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, collision.contacts[0].normal);
            var rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _enemyAttributes.RotationSpeed * Time.deltaTime);

            _targetDirection = rotation * Vector2.up;
        }
    }
}
