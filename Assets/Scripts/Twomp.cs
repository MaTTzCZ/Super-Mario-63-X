using UnityEngine;

using UnityEngine;

public class Thwomp : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private Transform player;
    [SerializeField] private float detectionWidth = 2f;
    [SerializeField] private float detectionHeight = 5f;
    [SerializeField] private float cooldownTime = 2f;

    [Header("Movement")]
    [SerializeField] private float fallSpeed = 15f;
    [SerializeField] private float returnSpeed = 3f;
    [SerializeField] private float waitTime = 1f;

    [Header("Audio")]
    [SerializeField] private AudioClip slamSound;

    
    private Vector3 startPosition;
    private bool isFalling;
    private bool isReturning;
    private Rigidbody2D rb;
    private float cooldownTimer;
    private bool isOnCooldown;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        startPosition = transform.position;
    }
    
    void Update()
    {
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                isOnCooldown = false;
            }
            return;
        }

        if (!isFalling && !isReturning)
        {
            CheckForPlayer();
        }

        if (isFalling)
        {
            Fall();
        }

        if (isReturning)
        {
            Return();
        }
    }

    private void CheckForPlayer()
    {
        if (player == null) return;

        // Je hráč pod nepřítelem?
        if (Mathf.Abs(player.position.x - transform.position.x) < detectionWidth &&
            player.position.y < transform.position.y &&
            player.position.y > transform.position.y - detectionHeight)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            isFalling = true;
        }
    }

    private void Fall()
    {
        rb.linearVelocity = Vector2.down * fallSpeed;
    }

    private void Return()
    {
        rb.linearVelocity = Vector2.up * returnSpeed;

        if (Vector2.Distance(transform.position, startPosition) < 0.05f)
        {
            rb.linearVelocity = Vector2.zero;
            transform.position = startPosition;
            isReturning = false;
            isOnCooldown = true;
            cooldownTimer = cooldownTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isFalling)
            {
                isFalling = false;
                rb.linearVelocity = Vector2.zero;
                if (SFXManager.instance != null)
                    SFXManager.instance.PlaySFXClip(slamSound, transform, 1);
                Invoke(nameof(StartReturn), waitTime);
            } 
        }
        
    }

    private void StartReturn()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        isReturning = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.down * (detectionHeight / 2),
            new Vector3(detectionWidth * 2, detectionHeight, 0));
    }
}
