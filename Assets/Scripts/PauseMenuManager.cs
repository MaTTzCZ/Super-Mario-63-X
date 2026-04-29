using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private InputActionReference pauseGameInputActionReference;
    public static PauseMenuManager Instance;

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

    private void OnEnable()
    {
        pauseGameInputActionReference.action.started = Toggle;
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.ReturnToMenu();
    }

    public void SwitchLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 0;
    }
}