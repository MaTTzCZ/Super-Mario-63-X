using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public void SwitchLevel(string sceneName)
    {
        GameManager.Instance.SwitchLevel(sceneName);
    }
}
