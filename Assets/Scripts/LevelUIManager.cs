using TMPro;
using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    public static LevelUIManager Instance;

    [SerializeField] private TMP_Text shineSprites;
    [SerializeField] private TMP_Text playerLives;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);
    }

    public void UpdateUI()
    {
        shineSprites.text = GameManager.Instance.shineSpriteCount.ToString();
        playerLives.text = GameManager.Instance.playerLives.ToString();
    }
}