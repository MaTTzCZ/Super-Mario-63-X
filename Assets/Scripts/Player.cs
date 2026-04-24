using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private TMP_Text playerLives;
    [SerializeField] private TMP_Text shineSpriteCount;

   


    void Start()
    {
        playerLives.text = GameManager.Instance.playerLives.ToString();
        shineSpriteCount.text = GameManager.Instance.shineSpriteCount.ToString();
    }
}