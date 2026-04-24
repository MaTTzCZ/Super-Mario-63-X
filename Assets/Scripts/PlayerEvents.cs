using System.Collections;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
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
        animator.SetBool(SpriteCollected, true);
        MusicManager.Instance.StopMusic();
        playerMovement.DisableMovement();
        do
        {
           yield return null; 
        } while (!playerMovement.IsGrounded());

        var audioSource = playerSFX.PlayShineSpriteCollectedSound(1);
        yield return new WaitForSeconds(audioSource.clip.length);
        LevelManager.Instance.RestartLevel();
        
    }
    
    public IEnumerator PlayerOutOfBounds()
    {
        PlayerDeath();
        var audioSource = playerSFX.PlayFallingSound(1);
        yield return new WaitForSeconds(audioSource.clip.length);
        LevelManager.Instance.RestartLevel();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shine Sprite"))
        {
            StartCoroutine(ShineSpriteCollected());
        }
    }

    private void PlayerDeath()
    {
        MusicManager.Instance.StopMusic();
        GameManager.Instance.playerLives--;
    }
}
