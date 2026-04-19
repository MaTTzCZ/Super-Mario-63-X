using UnityEngine;

public class OrbitManager : MonoBehaviour
{
    [SerializeField] private int count = 6;
    [SerializeField] private float radius = 4f;
    [SerializeField] private float speed = 30f;
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private Transform flippingPlatform;


    private Transform center;
    private GameObject[] platforms;

    void Start()
    {
        center = transform;
        platforms = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            float angle = i * Mathf.PI * 2f / count;

            Vector2 pos = new Vector2(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius
            );

            GameObject p = Instantiate(platformPrefab, center.position + (Vector3)pos, Quaternion.identity, transform);

            platforms[i] = p;
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < count; i++)
        {
            float baseAngle = i * Mathf.PI * 2f / count;
            float angle = baseAngle + Time.time * speed * Mathf.Deg2Rad;

            Vector2 pos = new Vector2(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius
            );

            Rigidbody2D rb = platforms[i].GetComponent<Rigidbody2D>();

            rb.MovePosition((Vector2)center.position + pos);
            
        }
        if (flippingPlatform != null)
            flippingPlatform.Rotate(0, 0, speed * Time.fixedDeltaTime);
    }
}