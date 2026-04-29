using System.Collections;
using UnityEngine;
using UnityEngine.AdaptivePerformance;
using UnityEngine.SceneManagement;

public class PlayerEvents : MonoBehaviour
{
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private LevelUIManager levelUIManager;
    private static readonly int SpriteCollected = Animator.StringToHash("ShineSpriteCollected");
    
    private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerSFX playerSFX;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerSFX = GetComponent<PlayerSFX>();
    }

    private IEnumerator ShineSpriteCollected()
    {
        levelUIManager.UpdateUI();
        animator.SetBool(SpriteCollected, true);
        musicManager.Stop();
        playerMovement.DisableMovement();
        do
        {
           yield return null; 
        } while (!playerMovement.IsGrounded());

        var audioSource = playerSFX.PlayShineSpriteCollectedSound();
        yield return new WaitForSeconds(audioSource.clip.length + 1);
        GameManager.Instance.ReturnToMenu();
    }

    private void OneUpMushroomCollected()
    {
        GameManager.Instance.playerLives++;
        levelUIManager.UpdateUI();
    }
    
    public IEnumerator PlayerOutOfBounds()
    {
        musicManager.Stop();
        var audioSource = playerSFX.PlayFallingSound();
        yield return new WaitForSeconds(audioSource.clip.length);
        GameManager.Instance.PlayerDeath(SceneManager.GetActiveScene().name);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shine Sprite"))
        {
            StartCoroutine(ShineSpriteCollected());
        }
        else if (other.CompareTag("1 Up Mushroom"))
        {
            OneUpMushroomCollected();
        }
    }
}
