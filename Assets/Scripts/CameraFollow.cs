using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;

    private readonly Vector3 _offset = new(0f, 0f, -10f);
    private Vector3 _velocity = Vector3.zero;

    private void Update()
    {
        if (target.position.x < Bounds.Instance.minX || target.position.x > Bounds.Instance.maxX ||
            target.position.y < Bounds.Instance.minY || target.position.y > Bounds.Instance.maxY)
            return;
        var targetPos = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, smoothTime);
    }
}