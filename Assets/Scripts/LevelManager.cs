using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public List<int> coinIDs;
    public List<int> redCoinIDs;
    
    void Start()
    {
        if(Instance == null)
            Instance = this;
    }
    
    public void ReturnToCastle()
    {
        SceneManager.LoadScene("Castle Main Hall");

    }
}
