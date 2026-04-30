using System.Collections;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float stepAngle = 90f;
    [SerializeField] private float waitTime = 1f;

    private void Start()
    {
        StartCoroutine(RotateStepRoutine());
    }

    private IEnumerator RotateStepRoutine()
    {
        while (true)
        {
            yield return StartCoroutine(RotateByStep());
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator RotateByStep()
    {
        float rotated = 0f;

        while (rotated < stepAngle)
        {
            float rotationThisFrame = rotationSpeed * Time.deltaTime;

            if (rotated + rotationThisFrame > stepAngle)
            {
                rotationThisFrame = stepAngle - rotated;
            }

            transform.Rotate(0, 0, rotationThisFrame);

            rotated += rotationThisFrame;

            yield return null;
        }
    }
}