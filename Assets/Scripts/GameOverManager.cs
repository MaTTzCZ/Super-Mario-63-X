using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private AudioClip gameOverSound;
    private void Start()
    {
        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine()
    {
        var audioSource = SFXManager.Instance.PlaySFXClip(gameOverSound, transform);
        yield return new WaitForSeconds(audioSource.clip.length + 1);
        SceneManager.LoadScene("Main Menu");
    }
}
