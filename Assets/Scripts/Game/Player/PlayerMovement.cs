using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private int _screenBorder;

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private bool _aimLockedToMovement = true;

    [SerializeField]
    private Texture2D _crosshairTexture;

    private Rigidbody2D _rigidbody;
    private Camera _camera;
    private Vector2 _movementInput;
    private Vector2 _dampenedMovementInput;
    private Vector2 _movementInputDampVelocity;
    private Animator _animator;
    private Vector2 _aimDirection;
    private PlayerInput _playerInput;

#if UNITY_WEBGL
    private Vector2 _previousMousePosition;
#endif

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        _camera = Camera.main;

        if (_aimLockedToMovement == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Vector2 crosshairCentre = new Vector2(_crosshairTexture.width / 2, _crosshairTexture.height / 2);
            Cursor.SetCursor(_crosshairTexture, crosshairCentre, CursorMode.Auto);
        }
    }

#if UNITY_WEBGL
    // For some reason the new Input System isn't working with WebGL, so had to revert to the old Input system
    private void Update()
    {
        _movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (_movementInput.magnitude > 1)
        {
            _movementInput.Normalize();
        }

        if (_aimLockedToMovement)
        {
            return;
        }

        Vector2 gamepadAimDirection = new Vector2(Input.GetAxis("AimHorizontal"), -Input.GetAxis("AimVertical"));
        Vector2 mousePosition = Input.mousePosition;

        if (gamepadAimDirection != Vector2.zero)
        {
            _aimDirection = gamepadAimDirection;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            if (_previousMousePosition != mousePosition)
            {
                Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(mousePosition);
                _aimDirection = mouseWorldPosition - transform.position;
                _aimDirection.Normalize();

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;                
            }
        }

        _previousMousePosition = mousePosition;
    }
#else
    private void OnMove(InputValue value)
    {
        _movementInput = value.Get<Vector2>();        
    }

    private void OnAim(InputValue value)
    {
        if (_aimLockedToMovement)
        {
            return;
        }

        if (_playerInput.currentControlScheme == "Gamepad")
        {
            Vector2 gamepadAimDirection = value.Get<Vector2>().normalized;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (gamepadAimDirection != Vector2.zero)
            {
                _aimDirection = gamepadAimDirection;
            }
        }
        else
        {
            Vector2 mousePosition = value.Get<Vector2>();
            Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(mousePosition);
            _aimDirection = mouseWorldPosition - transform.position;
            _aimDirection.Normalize();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
#endif

    private void FixedUpdate()
    {
        SetPlayerVelocity();

        if (_aimLockedToMovement)
        {
            RotateInDirectionOfInput();
        }
        else
        {
            RotateInDirectionOfAim();
        }

        SetAnimation();
    }

    private void SetAnimation()
    {
        bool isMoving = _movementInput != Vector2.zero;
        
        _animator.SetBool("IsMoving", isMoving);
    }

    private void SetPlayerVelocity()
    {
        _dampenedMovementInput = Vector2.SmoothDamp(_dampenedMovementInput, _movementInput, ref _movementInputDampVelocity, 0.1f);

        _rigidbody.velocity = _dampenedMovementInput * _speed;

        PreventPlayerGoingOffScreen();
    }

    private void PreventPlayerGoingOffScreen()
    {
        Vector2 screenPoint = _camera.WorldToScreenPoint(transform.position);

        if ((screenPoint.x < _screenBorder && _rigidbody.velocity.x < 0) ||
            (screenPoint.x > _camera.pixelWidth - _screenBorder && _rigidbody.velocity.x > 0))
        {
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        }

        if ((screenPoint.y < _screenBorder && _rigidbody.velocity.y < 0) ||
            (screenPoint.y > _camera.pixelHeight - _screenBorder && _rigidbody.velocity.y > 0))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        }
    }

    private void RotateInDirectionOfInput()
    {
        if (_movementInput != Vector2.zero)
        {
            Rotate(_dampenedMovementInput);
        }
    }

    private void RotateInDirectionOfAim()
    {
        Rotate(_aimDirection);
    }

    private void Rotate(Vector2 targetDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        var rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.MoveRotation(rotation);
    }
}
