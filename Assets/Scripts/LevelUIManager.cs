using TMPro;
using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text shineSprites;
    [SerializeField] private TMP_Text playerLives;

    public void UpdateUI()
    {
        shineSprites.text = GameManager.Instance.shineSpriteCount.ToString();
        playerLives.text = GameManager.Instance.playerLives.ToString();
    }
}