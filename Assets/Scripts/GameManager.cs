using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int playerLives = 5;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
