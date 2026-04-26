using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeReference] private PlayerMovement  playerMovement;
    public static MenuManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void ToggleMenu()
    {
        bool isActive = menu.activeSelf;
        menu.SetActive(!isActive);
        Time.timeScale = isActive ? 1 : 0;
        if (isActive)
            playerMovement.EnableMovement();
        else
            playerMovement.DisableMovement();
    }
}