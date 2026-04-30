using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private InputActionReference pauseGameInputActionReference;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject menu;

    private void OnEnable()
    {
        pauseGameInputActionReference.action.started += Toggle;
    }

    private void OnDisable()
    {
        pauseGameInputActionReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext obj)
    {
        var isActive = menu.activeSelf;
        menu.SetActive(!isActive);
        Time.timeScale = isActive ? 1 : 0;
        if (isActive)
            playerMovement.EnableMovement();
        else
            playerMovement.DisableMovement();
    }

    public void ReturnToMenu()
    {
        GameManager.Instance.ReturnToMenu();
    }

    public void SwitchLevel(string sceneName)
    {
        GameManager.Instance.SwitchLevel(sceneName);
    }
}