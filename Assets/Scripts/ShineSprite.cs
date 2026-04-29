using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShineSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public int id;
    [SerializeField] private Light2D light2D;
    [SerializeField] private Sprite yellowShineSprite;
    [SerializeField] private Sprite blueShineSprite;
    [SerializeField] private AudioClip shineSpriteAppears;
    [SerializeField] private AudioClip shineSpriteSparkle;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (GameManager.Instance.IsShineSpriteCollected(id))
        {
            spriteRenderer.sprite = blueShineSprite;
            light2D.color = Color.blue;
        }
        else
        {
            spriteRenderer.sprite = yellowShineSprite;
            light2D.color = Color.yellow;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CollectShineSprite(id);
            SFXManager.Instance.PlaySFXClip(shineSpriteSparkle, other.transform);
            Destroy(gameObject);
        }
    }
}
