using System.Collections;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;
    [SerializeField] private GameObject waypointsList;

    private Transform[] _waypoints;
    private int _pointIndex = 1;
    private int _direction = 1;
    private Transform _targetPosition;
    private bool _isWaiting;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _waypoints = new Transform[waypointsList.transform.childCount];
        for (var i = 0; i < waypointsList.transform.childCount; i++)
        {
            _waypoints[i] = waypointsList.transform.GetChild(i);
        }

        _targetPosition = _waypoints[_pointIndex];
    }

    private void FixedUpdate()
    {
        if (_isWaiting) 
            return;

        var step = speed * Time.fixedDeltaTime;

        var newPosition = Vector3.MoveTowards(
            _rb.position,
            _targetPosition.position,
            step
        );

        _rb.MovePosition(newPosition);

        if (Vector3.Distance(_rb.position, _targetPosition.position) < 0.01f)
        {
            StartCoroutine(WaitAndMove());
        }
    }

    private void NextPoint()
    {
        if (_pointIndex == _waypoints.Length - 1 || _pointIndex == 0)
        {
            _direction *= -1;
        }

        _pointIndex += _direction;
        _targetPosition = _waypoints[_pointIndex];
    }

    private IEnumerator WaitAndMove()
    {
        _isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        NextPoint();
        _isWaiting = false;
    }
}