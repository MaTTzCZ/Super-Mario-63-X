using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int playerLives = 5;
    public int shineSpriteCount;

    private readonly List<int> shineSpriteIDs = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SwitchLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }
    
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu");

    }

    public void PlayerDeath(string sceneName)
    {
        playerLives--;
        if (playerLives < 0)
            GameOver();
        else
            SceneManager.LoadScene(sceneName);
            
        
    }

    private void GameOver()
    {
        shineSpriteCount = 0;
        playerLives = 5;
        SceneManager.LoadScene("Game Over");
    }
    
    public void CollectShineSprite(int shineSpriteID)
    {
        if (shineSpriteIDs.Contains(shineSpriteID)) return;
        shineSpriteIDs.Add(shineSpriteID);
        shineSpriteCount++;
    }
    public bool IsShineSpriteCollected(int shineSpriteID)
    {
        return shineSpriteIDs.Contains(shineSpriteID);
    }
}