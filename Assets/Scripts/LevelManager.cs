using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public List<int> coinIDs;
    public List<int> redCoinIDs;
    void Start()
    {
        if(instance == null)
            instance = this;
    }

    void Update()
    {
        
    }
}
