using UnityEngine;
using System;

public class PlayerController : MonoBehaviour 
{
    public event Action OnPlayerJumped;
    private Rigidbody _playerRigidbody; 

    [Header("References")]
    [SerializeField] private Transform _orientationTransform;

    [Header("Speed")]
    [SerializeField] private KeyCode _movemetKey;
    [SerializeField] private float _movementSpeed = 10f;

    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _jumpCooldown = 0.25f;
    [SerializeField] private float _airMultiplier;
    [SerializeField] private float _airDrag;
    [SerializeField] private bool _canjump = true; 

    [Header("Sliding Settings")]
    [SerializeField] private KeyCode _SlideKey;
    [SerializeField] private float _slideMultiplier;
    [SerializeField] private float _SlideDrag;

    [Header("Ground Check Settings")]
    [SerializeField] private float _PlayerHeight = 2f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _GroundDrag;
    private StateController _stateController;

    private float _horizontalInput, _verticalInput;
    private Vector3 _movementDirection;
    private bool _isSliding;

    private void Awake()
    {
        _stateController= GetComponent<StateController>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
    }

    private void Update()
    {
        SetInputs();
        SetStates();
        SetPlayerDrag();
        LimitPlayerSpeed();  
    } 

    private void FixedUpdate()
    {
        SetPlayerMovement();
    }

    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(_SlideKey))
        {
            _isSliding = true;
        }
        else if(Input.GetKeyDown(_movemetKey))
        {
            _isSliding = false;
        }
        // Not: Yukarıdaki else if yapısı nedeniyle zıplama kontrolünü ayrı if bloğuna almak daha sağlıklıdır.
        if (Input.GetKeyDown(_jumpKey) && _canjump && IsGrounded())
        {
            _canjump = false;
            SetPlayerJumping();
            Invoke(nameof(ResetJumping), _jumpCooldown);
        }
    }
    private void SetStates(){
        var movementDirection=GetMovementDirection();
        var isGrounded=IsGrounded();
        var isSliding=IsSliding();
        var currenState=_stateController.GetCurrentState();
        var newState= currenState switch{
             _ when movementDirection == Vector3.zero && isGrounded && !isSliding => PlayerState.Idle,
             _ when movementDirection != Vector3.zero && isGrounded && !isSliding => PlayerState.Move,
             _ when movementDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,
             _ when movementDirection == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,
             _ when !_canjump && !isGrounded =>PlayerState.Jump,
             _ => currenState
        };
        if(newState != currenState)
        {
            _stateController.ChangeState(newState);
        }
    
    }

    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;
        float forceMultiplier = _stateController.GetCurrentState() switch{
            PlayerState.Move => 1f,
            PlayerState.Slide => _slideMultiplier,
            PlayerState.Jump => _airMultiplier,
            _ => 1f
        };
        _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed *forceMultiplier, ForceMode.Force);
    }

    private void SetPlayerDrag()
    {
        _playerRigidbody.linearDamping = _stateController.GetCurrentState() switch{
        PlayerState.Move =>_GroundDrag,
        PlayerState.Slide =>_SlideDrag,
        PlayerState.Jump =>_airDrag,
        _ => _playerRigidbody.linearDamping
        };
       
    }

    private void LimitPlayerSpeed()
    {
        // 1. HATA DÜZELTİLDİ: Satır sonuna ; eklendi
        Vector3 flatVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);

        if(flatVelocity.magnitude > _movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
            // 2. HATA DÜZELTİLDİ: "limitedVelocity" diye bir rigidbody özelliği yoktur, doğrusu linearVelocity.
            _playerRigidbody.linearVelocity = new Vector3(limitedVelocity.x, _playerRigidbody.linearVelocity.y, limitedVelocity.z);
        }
    }

    private void SetPlayerJumping()
    {
        OnPlayerJumped?.Invoke();
        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void ResetJumping()
    {
        _canjump = true;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _PlayerHeight * 0.5f + 0.2f, _groundLayer);
    }
    private Vector3 GetMovementDirection(){
        return _movementDirection.normalized;
    }
    private bool IsSliding(){
        return _isSliding;
    }
}