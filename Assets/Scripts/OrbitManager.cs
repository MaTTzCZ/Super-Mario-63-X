using UnityEngine;

public class OrbitManager : MonoBehaviour
{
    [SerializeField] private int count = 6;
    [SerializeField] private float radius = 4f;
    [SerializeField] private float speed = 30f;
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private Transform flippingPlatform;


    private Transform center;
    private Rigidbody2D[] rbs;
    private bool hasFlippingPlatform;

    void Start()
    {
        center = transform;
        rbs = new Rigidbody2D[count];
        if (flippingPlatform != null)
            hasFlippingPlatform = true;

        for (var i = 0; i < count; i++)
        {
            var angle = i * Mathf.PI * 2f / count;

            var pos = new Vector2(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius
            );

            var p = Instantiate(platformPrefab, center.position + (Vector3)pos, Quaternion.identity, transform);
            rbs[i] = p.GetComponent<Rigidbody2D>();
        }
    }

    void FixedUpdate()
    {
        for (var i = 0; i < count; i++)
        {
            var baseAngle = i * Mathf.PI * 2f / count;
            var angle = baseAngle + Time.time * speed * Mathf.Deg2Rad;

            var pos = new Vector2(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius
            );
            rbs[i].MovePosition((Vector2)center.position + pos);
        }
        if (hasFlippingPlatform)
            flippingPlatform.Rotate(0, 0, speed * Time.fixedDeltaTime);
    }
}