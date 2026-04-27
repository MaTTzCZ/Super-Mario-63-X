using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeReference] private InputActionReference pauseActionReference;
    public static GameManager Instance;
    public int playerLives = 5;
    public int shineSpriteCount = 0;

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

    public void GameOver()
    {
        shineSpriteCount = 0;
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

    private void OnEnable()
    {
        pauseActionReference.action.started += Pause;
    }

    private void OnDisable()
    {
        pauseActionReference.action.started -= Pause;
    }

    private void Pause(InputAction.CallbackContext obj)
    { 
        MenuManager.Instance.ToggleMenu();
    }
}