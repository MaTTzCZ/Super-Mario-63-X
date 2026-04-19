using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private InputActionReference movement;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference slide;

    [SerializeField] private Transform headCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    [SerializeField] private float tripleJumpForce;

    [SerializeField] private PhysicsMaterial2D idleMaterial;
    [SerializeField] private PhysicsMaterial2D moveMaterial;
    
    [SerializeField] private AudioClip[] marioJump;
    [SerializeField] private AudioClip marioDoubleJump;
    [SerializeField] private AudioClip[] marioTripleJump;
    [SerializeField] private AudioClip marioFalling;

    private Rigidbody2D rb;
    private Animator animator;

    private float _moveDirection;
    private bool _isFacingRight = true;
    private bool _isSliding;
    private int _jumpStage = 0;
    private double timeOfLastJump;
    private bool isJumping;


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
        animator.SetFloat("X_Speed", Mathf.Abs(_moveDirection));
        animator.SetFloat("Y_Speed", rb.linearVelocity.y);
        animator.SetBool("IsGrounded", IsGrounded());
        animator.SetBool("IsSliding", _isSliding);
        animator.SetInteger("JumpStage", _jumpStage);
        if (transform.position.y < Bounds.Instance.minY)
        {
            Debug.Log("Game Over");
            Scene currtScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currtScene.name);
        }
        if (IsMoving() && !_isSliding)
        {
            rb.sharedMaterial = moveMaterial;
        }
        else
        {
            rb.sharedMaterial = idleMaterial;
        }

        if (_isSliding && !IsGrounded())
        {
            _isSliding = true;
        }
        
        if (Time.time - timeOfLastJump > 1.2f)
        {
            _jumpStage = 0;
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
        if (!IsGrounded()) return;

        _jumpStage++;

        float currentJumpForce = jumpForce;

        if (_jumpStage == 1)
        {
            // první skok
            SFXManager.instance.PlayRandomSFXClip(marioJump, transform, 1);
        }
        else if (_jumpStage == 2)
        {
            // druhý skok
            SFXManager.instance.PlaySFXClip(marioDoubleJump, transform, 1);
            currentJumpForce = doubleJumpForce;
        }
        else if (_jumpStage == 3)
        {
            if (!IsMoving())
            {
                _jumpStage = 2; // nedovol triple jump
                return;
            }

            SFXManager.instance.PlayRandomSFXClip(marioTripleJump, transform, 1);
            currentJumpForce = tripleJumpForce;
            animator.SetTrigger("TripleJump");

            _jumpStage = 0;
        }


        rb.AddForce(Vector2.up * currentJumpForce, ForceMode2D.Impulse);
        isJumping = true;
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
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        if (isJumping && isGrounded)
        {
            timeOfLastJump = Time.time;
            isJumping = false;
            
        }
        return isGrounded;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Thwomp"))
        {
            transform.localScale = new Vector3(-transform.localScale.x, 0.09f, transform.localScale.z);
        }
    }
}