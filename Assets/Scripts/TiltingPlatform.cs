using UnityEngine;

public class TiltingPlatform : MonoBehaviour
{
    private bool _isPlayerOnPlatform;
    [SerializeField] private Transform player;
    [SerializeField] private float maxAngle = 20f;
    [SerializeField] private float rotationSpeed = 5f;

    private void Update()
    {
        if (_isPlayerOnPlatform)
        {
            var direction = player.position.x - transform.position.x;
            var targetAngle = Mathf.Clamp(direction * maxAngle, -maxAngle, maxAngle);
            var targetRotation = Quaternion.Euler(0, 0, -targetAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * rotationSpeed);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayerOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayerOnPlatform = false;
        }
    }
}