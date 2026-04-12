using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private InputActionReference movement;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference slide;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [SerializeField] private PhysicsMaterial2D idleMaterial;
    [SerializeField] private PhysicsMaterial2D moveMaterial;

    private Rigidbody2D rb;
    private Animator animator;

    private float _moveDirection;
    private bool _isFacingRight = true;
    private bool _isSliding;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _moveDirection = movement.action.ReadValue<float>();
        _isSliding = slide.action.IsPressed();
        animator.SetFloat("X_Speed", Mathf.Abs(_moveDirection * 1.7f));
        animator.SetFloat("Y_Speed", rb.linearVelocity.y);
        animator.SetBool("IsGrounded", IsGrounded());
        animator.SetBool("IsSliding", _isSliding);
        if (IsMoving())
        {
            rb.sharedMaterial = moveMaterial;
        }
        else
        {
            rb.sharedMaterial = idleMaterial;
        }
    }

    void FixedUpdate()
    {
        if (!_isSliding)
        {
            Move();
            Flip();
        }
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(_moveDirection * speed, rb.linearVelocity.y);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
            rb.AddForce(new Vector2(0, jumpForce));
    }

    private void Flip()
    {
        if (_moveDirection < 0 && _isFacingRight || _moveDirection > 0 && !_isFacingRight)
        {
            _isFacingRight = !_isFacingRight;
            var localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private void OnEnable()
    {
        jump.action.started += Jump;
    }

    private void OnDisable()
    {
        jump.action.started -= Jump;
    }

    private bool IsMoving()
    {
        return Mathf.Abs(_moveDirection) > 0.1f;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}