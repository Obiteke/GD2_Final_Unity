using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerScript : MonoBehaviour
{
    #region Fields
    private Vector3 _moveDir = Vector3.zero;
    private Vector2 _moveInput;
    private bool _isMoving = false;
    private bool _isJumping = false;
    private bool _isJumpingPressed;
    //private Animator _animator;
    private CharacterController _cc;
    
    public float Speed;

    private float _gravity = -9.81f;
    private float _groundedGravity = -0.05f;

    private float _initialJumpVelocity;
    private float _maxJumpHeight = 4f;
    private float _maxJumpTime = 0.75f;
    #endregion

    #region Properties
    public bool IsMoving { get => _isMoving; set => _isMoving = value; }
    #endregion

    #region Methods
    private void Awake()
    {
        //_animator = gameObject.GetComponentInChildren<Animator>();
        _cc = gameObject.GetComponent<CharacterController>();
        setupJumpVariables();
    }
    private void setupJumpVariables()
    {
        float timeToApex = _maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
    }

    void FixedUpdate()
    {
        Movement();
        Rotation();
        _cc.Move(_moveDir * Time.fixedDeltaTime);
        HandleGravity();
        HandleJump();
    }

    public void HandleGravity()
    {
        bool isFalling = _moveDir.y <= 0f || !_isJumpingPressed;
        float fallMultiplier = 2.0f;

        if (_cc.isGrounded)
            _moveDir.y = _groundedGravity;
        else if (isFalling)
        {
            float previousYVelocity = _moveDir.y;
            float newYVelocity = _moveDir.y + (_gravity * fallMultiplier * Time.fixedDeltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            _moveDir.y = nextYVelocity;
        }
        else
        {
            float previousYVelocity = _moveDir.y;
            float newYVelocity = _moveDir.y + (_gravity * Time.fixedDeltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            _moveDir.y = nextYVelocity;
        }
        //https://www.youtube.com/watch?v=h2r3_KjChf4 video about jumping
    }
    public void MovementInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();

        if (context.performed)
            IsMoving = true;

        if (context.canceled)
            IsMoving = false;
    }

    public void JumpingInput(InputAction.CallbackContext context)
    {
        _isJumpingPressed = context.ReadValueAsButton();
    }
    private void HandleJump()
    {
        if (!_isJumping && _isJumpingPressed && _cc.isGrounded)
        {
            _isJumping = true;
            _moveDir.y = _initialJumpVelocity * .5f;
        }
        else if(!_isJumpingPressed && _isJumping &&_cc.isGrounded)
        {
            _isJumping = false;
        }
    }


    private void Movement()
    {
        if (IsMoving)
        {
            if (_moveInput.x != 0 || _moveInput.y != 0)
            {
                _moveDir =  new Vector3(Speed * _moveInput.x, _moveDir.y, Speed * _moveInput.y);
                //_animator.SetBool("isWalking", true);
            }
        }
        else
        {
            //_animator.SetBool("isWalking", false);
            _moveDir = new Vector3(0, _moveDir.y, 0);
        }

    }
    private void Rotation()
    {
        if (IsMoving)
        {
            if (_moveInput.x != 0 || _moveInput.y != 0)
            {
                float angle = Mathf.Atan2(_moveInput.x, _moveInput.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
        }
    }
    #endregion
}
