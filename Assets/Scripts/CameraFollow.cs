using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float minX, maxX, minY, maxY;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;

    private readonly Vector3 _offset = new(0f, 0f, -10f);
    private Vector3 _velocity = Vector3.zero;

    private void Update()
    {
        if (target.position.x < minX || target.position.x > maxX  || target.position.y < minY || target.position.y > maxY)
            return;
        var targetPos = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, smoothTime);
    }
}