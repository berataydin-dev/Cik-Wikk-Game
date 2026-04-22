using UnityEngine;

public class PlayerController : MonoBehaviour 
{
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
    [SerializeField] private bool _canjump = true; 

    [Header("Sliding Settings")]
    [SerializeField] private KeyCode _SlideKey;
    [SerializeField] private float _SlideMultiplier;
    [SerializeField] private float _SlideDrag;

    [Header("Ground Check Settings")]
    [SerializeField] private float _PlayerHeight = 2f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _GroundDrag;

    private float _horizontalInput, _verticalInput;
    private Vector3 _movementDirection;
    private bool _isSliding;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
    }

    private void Update()
    {
        SetInputs();
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

    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;
        if(_isSliding)
        {
            _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed * _SlideMultiplier, ForceMode.Force);
        }
        else 
        {
            _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed, ForceMode.Force);
        }
    }

    private void SetPlayerDrag()
    {
        if(_isSliding)
        {
            _playerRigidbody.linearDamping = _SlideDrag;
        }
        else
        {
            _playerRigidbody.linearDamping = _GroundDrag;
        }
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
}