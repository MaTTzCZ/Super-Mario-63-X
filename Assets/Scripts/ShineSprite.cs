using UnityEngine;

public class ShineSprite : MonoBehaviour
{
    [SerializeField] private int ID;
    [SerializeField] private AudioClip shineSpriteAppears;
    [SerializeField] private AudioClip shineSpriteSparkle;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SFXManager.instance.PlaySFXClip(shineSpriteSparkle, other.transform, 1);
            Destroy(gameObject);
        }
    }
}
