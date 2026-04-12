using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    
    private Vector3 _offset = new Vector3(0f, 0f, -10f);
    private Vector3 _velocity = Vector3.zero;
    void Update()
    {
        var targetPos = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, smoothTime);
    }
}
