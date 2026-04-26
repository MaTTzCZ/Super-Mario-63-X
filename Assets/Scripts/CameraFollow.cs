using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float minX, maxX, minY, maxY;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;

    private readonly Vector3 offset = new(0f, 0f, -10f);
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        var targetPos = target.position + offset;
        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);
        var newX = Mathf.SmoothDamp(transform.position.x, targetPos.x, ref velocity.x, smoothTime);
        var newY = Mathf.SmoothDamp(transform.position.y, targetPos.y, ref velocity.y, smoothTime);
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}