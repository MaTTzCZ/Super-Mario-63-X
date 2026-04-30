using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private SFXManager SFXManager;
    [SerializeField] private AudioClip gameOverSound;
    private void Start()
    {
        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        var audioSource = SFXManager.PlaySFXClip(gameOverSound, transform);
        yield return new WaitForSeconds(audioSource.clip.length + 1);
        SceneManager.LoadScene("Main Menu");
    }
}
