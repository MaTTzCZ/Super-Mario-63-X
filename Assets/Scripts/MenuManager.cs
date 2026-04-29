using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeReference] private PlayerMovement playerMovement;

    public void ToggleMenu()
    {
        var isActive = menu.activeSelf;
        menu.SetActive(!isActive);
        Time.timeScale = isActive ? 1 : 0;
        if (isActive)
            playerMovement.EnableMovement();
        else
            playerMovement.DisableMovement();
    }
}