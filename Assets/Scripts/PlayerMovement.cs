using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int HorizontalSpeed = Animator.StringToHash("HorizontalSpeed");
    private static readonly int VerticalSpeed = Animator.StringToHash("VerticalSpeed");
    private static readonly int Grounded = Animator.StringToHash("IsGrounded");
    
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerSFX playerSFX;
    private PlayerEvents playerEvents;

    private float moveDirection;
    private bool isFacingRight = true;
    private int jumpStage;
    private double lastJumpTime;
    private bool isDead;
    private bool canFallOutOfBounds; 

    [Header("Input Action References")] 
    [SerializeField] private InputActionReference moveActionReference;
    [SerializeField] private InputActionReference jumpActionReference;
    
    [Header("Movement")] 
    [Range(0.1f,10)][SerializeField] private float speed;

    [Header("Surroundings Check")] 
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
    [Header("Jump")] 
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    [SerializeField] private float tripleJumpForce;
    
    [Header("Materials")]
    [SerializeField] private PhysicsMaterial2D idleMaterial;
    [SerializeField] private PhysicsMaterial2D moveMaterial;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerSFX = GetComponent<PlayerSFX>();
        playerEvents = GetComponent<PlayerEvents>();
        isDead = false;
        canFallOutOfBounds = Bounds.Instance != null;
    }

    private void Update()
    {
        moveDirection = moveActionReference.action.ReadValue<float>();
        animator.SetFloat(HorizontalSpeed, Mathf.Abs(moveDirection));
        animator.SetFloat(VerticalSpeed, rb.linearVelocity.y);
        animator.SetBool(Grounded, IsGrounded());
        if (!canFallOutOfBounds || !IsOutOfBounds() || isDead) return;
        isDead = true;
        StartCoroutine(playerEvents.PlayerOutOfBounds());
    }

    private void FixedUpdate()
    {
        Move();
        Flip();
        ChangeMaterial();
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (!IsGrounded()) return;
        var currentJumpForce = jumpForce;
        if (jumpStage == 0)
        {
            playerSFX.PlayJumpSound();
            jumpStage++;
        }
        else if (jumpStage == 1)
        {
            playerSFX.PlayDoubleJumpSound();
            currentJumpForce = doubleJumpForce;
            jumpStage++;
        }
        else if (jumpStage == 2)
        {
            if (!IsMoving())
            {
                playerSFX.PlayDoubleJumpSound();
                currentJumpForce = doubleJumpForce;
            }
            else
            {
                playerSFX.PlayTripleJumpSound();
                currentJumpForce = tripleJumpForce;
                jumpStage = 0;
            }
        }
        rb.AddForce(Vector2.up * currentJumpForce, ForceMode2D.Impulse);
    }

    private void Flip()
    {
        if (moveDirection > 0 && !isFacingRight || moveDirection < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            var localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    public void DisableMovement()
    {
        moveActionReference.action.Disable();
        jumpActionReference.action.Disable();
    }

    public void EnableMovement()
    {
        moveActionReference.action.Enable();
        jumpActionReference.action.Enable();
    }

    private void OnEnable()
    {
        jumpActionReference.action.started += Jump;
    }

    private void OnDisable()
    {
        jumpActionReference.action.started -= Jump;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private bool IsOutOfBounds()
    {
        return Bounds.Instance.minX > transform.position.x || 
               Bounds.Instance.maxX < transform.position.x || 
               Bounds.Instance.minY > transform.position.y;

    }
    
    private bool IsMoving()
    {
        return Mathf.Abs(moveDirection) > 0.1f;
    }

    private void ChangeMaterial()
    {
        rb.sharedMaterial = IsMoving() ? moveMaterial : idleMaterial;
    }
}